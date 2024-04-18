using LoggiFin.Data;
using LoggiFin.DBO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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

        public async Task<Boolean> postDatarow(JsonDatarow datarow, int uid)
        {
            if(datarow == null) return false;

            JsonObject json = new JsonObject();
            json.Add("uid", uid);
            json.Add("date", datarow.GetDate().Year + "" + datarow.GetDate().Month.ToString().PadLeft(2, '0') + datarow.GetDate().Day.ToString().PadLeft(2, '0'));
            json.Add("einnahmen", datarow.Einnahmen);
            json.Add("ausgaben", datarow.Ausgaben);
            json.Add("kontostand", datarow.Kontostand);
            json.Add("sparkontostand", datarow.Sparkontostand);
            json.Add("volksbankSparplan", datarow.VolksbankSparplan);
            json.Add("scalableSparplan", datarow.ScalableSparplan);
            json.Add("volksbankSparplanTotal", datarow.VolksbankSparplanTotal);
            json.Add("scalableSparplanTotal", datarow.ScalableSparplanTotal);
            json.Add("beschreibung", datarow.Beschreibung);
            json.Add("amountTotal", datarow.TransferMenge);
            json.Add("bargeld", datarow.Bargeld);
            json.Add("urlaubskasse", datarow.Urlaubskasse);

            HttpResponseMessage response = await client.PostAsJsonAsync("loggifin/dataRow", json);

            if(response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Boolean> deleteDatarow(int rowId, int uid)
        {
            HttpResponseMessage response = await client.DeleteAsync("loggifin/dataRow?uid=" + uid + "&rowid=" + rowId);

            if(response.StatusCode == HttpStatusCode.Accepted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
