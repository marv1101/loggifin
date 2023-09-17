using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggiFin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LoggiFin.DBO;

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

        [TestMethod()]
        public void GetAbokostenTest()
        {
            //given
            FinLoggingDataService loggingDataService = new FinLoggingDataService();
            //when
            List<Abokosten> res = loggingDataService.GetAbokosten();
            //then
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Count() == 0);
            Assert.IsTrue(res.First().kosten != 0);

            Trace.WriteLine(res.First().titel);
        }

        [TestMethod()]
        public void getTransferTextTest()
        {
            //given
            //when
            string res = FinLoggingDataService.getTransferText(1);
            //then
            Assert.IsNotNull(res);
            Console.WriteLine(res);
        }
    }
}