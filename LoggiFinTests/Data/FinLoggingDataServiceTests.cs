using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggiFin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LoggiFin.Data.Tests
{
    [TestClass()]
    public class FinLoggingDataServiceTests
    {
        [TestMethod()]
        public void FinLoggingDataServiceTest()
        {
            //given
            FinLoggingDataService loggingDataService = new FinLoggingDataService();
            //when
            IEnumerable<FinLoggingDataRow> res = loggingDataService.GetDataRows();
            //then
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Count() == 0);
            Assert.IsTrue(res.First().beschreibung.Length != 0);

            Trace.WriteLine(res.First().beschreibung);
        }
    }
}