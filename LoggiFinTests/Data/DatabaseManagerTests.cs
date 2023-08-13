using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggiFin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggiFin.DBO;
using System.Diagnostics;
using Org.BouncyCastle.Utilities;
using BlazorBootstrap.Extensions;

namespace LoggiFin.Data.Tests
{
    [TestClass()]
    public class DatabaseManagerTests
    {


        [TestMethod()]
        public void getDataTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            List<Finanzdaten> res = manager.getData();
            Finanzdaten fstElem = res.Last();

            //then
            Assert.IsFalse(res.Count == 0);
            Assert.IsTrue(fstElem != null);
            Assert.IsTrue(fstElem.datasetID_PK != 0);
            Assert.IsTrue(fstElem.kontodatenID_FK != 0);
            Assert.IsTrue(fstElem.investdatenID_FK != 0);
            Assert.IsTrue(fstElem.transferdatenID_FK != 0);
            Assert.IsTrue(fstElem.bargelddatenID_FK != 0);
            Assert.IsTrue(fstElem.entryDate.Year > 2000);

            Trace.WriteLine(fstElem.datasetID_PK);
            Trace.WriteLine(fstElem.entryDate);
        }

        [TestMethod()]
        public void getKontodatenTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            int kontodatenID_PK = 1;
            Kontodaten res = manager.getKontodaten(kontodatenID_PK);

            //then
            Assert.IsTrue(res != null);
            Assert.IsTrue(res.kontodatenID_PK == kontodatenID_PK);
            Assert.IsTrue(res.ausgaben != 0);
            Assert.IsTrue(res.einnahmen != 0);

            Trace.WriteLine(res.ausgaben);
        }

        [TestMethod()]
        public void getInvestdatenTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            int investdatenID_PK = 1;
            Investmentdaten res = manager.getInvestdaten(investdatenID_PK);

            //then
            Assert.IsTrue(res != null);
            Assert.IsTrue(res.investdatenID_PK == investdatenID_PK);
            Assert.IsTrue(res.volksbankStand != double.MinValue);
            Assert.IsTrue(res.scalableStand != double.MinValue);

            Trace.WriteLine(res.scalableStand);
        }

        [TestMethod()]
        public void GetTransferdatenTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            int transferdatenID_PK = 1;
            Transferdaten res = manager.getTransferdaten(transferdatenID_PK);

            //then
            Assert.IsTrue(res != null);
            Assert.IsTrue(res.transferdatenID_PK == transferdatenID_PK);
            Assert.IsFalse(string.IsNullOrEmpty(res.beschreibung));
            Assert.IsTrue(res.amountTotal != double.MinValue);

            Trace.WriteLine(res.beschreibung);
        }

        [TestMethod()]
        public void getBargelddatenTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            int bargelddatenID_PK = 1;
            Bargelddaten res = manager.getBargelddaten(bargelddatenID_PK);

            //then
            Assert.IsTrue(res != null);
            Assert.IsTrue(res.bargelddatenID_PK == bargelddatenID_PK);
            Assert.IsTrue(res.bargeld != double.MinValue);
            Assert.IsTrue(res.urlaubskasse != double.MinValue);

            Trace.WriteLine(res.urlaubskasse);
        }

        [TestMethod()]
        public void insertDataIntoDbTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            Random rnd = new Random();
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            double einnahmen = rnd.NextDouble() * 1500;
            double ausgaben = rnd.NextDouble() * 1500;
            double kontostand = rnd.NextDouble() * 50000;
            double sparkontostand = rnd.NextDouble() * 100000;
            double scalable = rnd.NextDouble() * 100000;
            double volksbank = rnd.NextDouble() * 2000;
            string transf = "TestTransfer";
            double transfAmount = rnd.NextDouble() * einnahmen;
            double bargeld = rnd.NextDouble() * 500;
            double urlaubskasse = rnd.NextDouble() * 1000;
            //when
            bool res = manager.insertDataIntoDb(date, einnahmen, ausgaben, kontostand,
                sparkontostand, scalable, volksbank, transf, transfAmount, bargeld, urlaubskasse);
            //then
            Assert.IsTrue(res);
        }
    }
}