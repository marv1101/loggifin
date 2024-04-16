using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggiFin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggiFin.Data;
using LoggiFin.DBO;

namespace LoggiFin.Services.Tests
{
    [TestClass()]
    public class CSVExporterTests
    {
        [TestMethod()]
        public async Task createFileTest()
        {
            APIConnector apiConnector = new APIConnector();

            IEnumerable<JsonDatarow> rows = await apiConnector.getDataRowsAsync(1);

            Assert.IsNotNull(rows);
            Assert.IsTrue(rows.Any());
            Assert.IsTrue(rows.Count() != 0);

            CSVExporter csvExporter = new CSVExporter(rows.ToList());

            Boolean res = csvExporter.createFile();

            Assert.IsTrue(res);
        }
    }
}