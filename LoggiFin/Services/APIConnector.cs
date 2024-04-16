using LoggiFin.Data;
using LoggiFin.DBO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace LoggiFin.Services
{
    public class APIConnector
    {

        HttpClient client;

        public APIConnector()
        {
            client = new HttpClient();
            Uri baseUri = new Uri("http://10.69.1.11:8085/lbe-1_4/api/");

            client.BaseAddress = baseUri;
        }

        public async Task<List<JsonDatarow>> getDataRowsAsync(int uid)
        {
            HttpResponseMessage response = await client.GetAsync("loggifin/dataRow?uid="+uid);

            Task<string> res = response.Content.ReadAsStringAsync();

            List<JsonDatarow?>? datarows = new List<JsonDatarow?>();

            string? resultData = res.Result;

            try
            {
                datarows = JsonSerializer.Deserialize<List<JsonDatarow?>>(resultData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            foreach(JsonDatarow row in datarows)
            {
                if (row == null) datarows.Remove(row);
            }

            return datarows;
        }

        public async Task<List<JsonDatarow>> getDataRowsAsync(int uid, DateOnly from, DateOnly to)
        {
            HttpResponseMessage response = await client.GetAsync("loggifin/dataRow?uid=" + uid + "&from=" + from.Year + from.Month.ToString().PadLeft(2, '0') + from.Day.ToString().PadLeft(2, '0') + 
                "&to=" + to.Year + to.Month.ToString().PadLeft(2, '0') + from.Day.ToString().PadLeft(2, '0'));
            Task<string> res = response.Content.ReadAsStringAsync();

            List<JsonDatarow?>? datarows = new List<JsonDatarow?>();

            string? resultData = res.Result;

            try
            {
                datarows = JsonSerializer.Deserialize<List<JsonDatarow?>>(resultData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            foreach (JsonDatarow row in datarows)
            {
                if (row == null) datarows.Remove(row);
            }

            return datarows;
        }

    }
}
