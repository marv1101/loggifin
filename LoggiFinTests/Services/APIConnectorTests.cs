using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggiFin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LoggiFin.DBO;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace LoggiFin.Services.Tests
{
    [TestClass()]
    public class APIConnectorTests
    {
        [TestMethod()]
        public async Task getDataRowsAsyncTest()
        {
            APIConnector connector = new APIConnector();

            Assert.IsNotNull(connector);

            List<JsonDatarow> res = await connector.getDataRowsAsync(1);

            Assert.IsTrue(res.Any());
            Assert.IsTrue(res[0].GetDate() != null);

            Trace.WriteLine(res.Count);
            Trace.WriteLine(res[res.Count - 1].ToString());
            Trace.WriteLine(res[0].GetDate());
        }

        [TestMethod()]
        public async Task postDatarowTest()
        {
            APIConnector connector = new APIConnector();

            Assert.IsNotNull(connector);

            JsonObject json = new JsonObject();
            json.Add("nix", 1);

            Boolean res = await connector.postDatarow(null, 1);

            Assert.IsFalse(res);

            json = new JsonObject();
            json.Add("date", DateTime.Now.Year + "" + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0'));
            json.Add("einnahmen", 40);
            json.Add("ausgaben", 30);
            json.Add("kontostand", 90);
            json.Add("sparkontostand", 100);
            json.Add("volksbankSparplan", 70);
            json.Add("scalableSparplan", 200);
            json.Add("volksbankSparplanTotal", 80);
            json.Add("scalableSparplanTotal", 170);
            json.Add("amountTotal", 10);
            json.Add("bargeld", 10);
            json.Add("urlaubskasse", 10);

            JsonDatarow? datarow = JsonSerializer.Deserialize<JsonDatarow>(json);

            datarow.date = DateOnly.FromDateTime(DateTime.Now);
            datarow.Beschreibung= "Testiiiiing";
            Trace.WriteLine(datarow.GetDate());

            res = await connector.postDatarow(datarow, 1);

            Assert.IsTrue(res);

        }

        [TestMethod()]
        public async Task deleteDatarowTest()
        {
            APIConnector connector = new APIConnector();

            Assert.IsNotNull(connector);

            Boolean res = await connector.deleteDatarow(147, 1);

            Assert.IsNotNull(res);
        }
    }
}