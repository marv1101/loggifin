using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggiFin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LoggiFin.DBO;

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
            Trace.WriteLine(res[res.Count-1].ToString());
            Trace.WriteLine(res[0].GetDate());
        }
    }
}