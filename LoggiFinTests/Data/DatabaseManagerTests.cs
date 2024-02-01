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

        [TestMethod()]
        public void authUserTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            bool res = manager.authUser("testuser", "test");
            bool res2 = manager.authUser("testuser1", "test");

            //then
            Assert.IsTrue(res);
            Assert.IsFalse(res2);
        }

        [TestMethod()]
        public void getUserIdByNameTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            int res = manager.getUserIdByName("testuser", "test");
            int res2 = manager.getUserIdByName("testuser1", "test");

            //then
            Assert.AreEqual(1, res);
            Assert.IsFalse(res2 >= 0);
        }

        [TestMethod()]
        public void removeDataTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            bool res = manager.removeData(41);

            //then
            Assert.IsFalse(res);

        }

        [TestMethod()]
        public void getAbokostenTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);
            //when
            List<Abokosten> abokosten = manager.getAbokosten(1);
            Abokosten abo = abokosten.First();
            //then
            Assert.IsNotNull(abokosten);
            Assert.IsFalse(abokosten.Count == 0);
            Assert.IsNotNull(abo);
            Assert.AreEqual(abo.titel, "Testabo");
            Assert.AreEqual(abo.aboID_PK, 1);

        }

        [TestMethod()]
        public void insertAboKostenIntoDbTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            Random rnd = new Random();
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            double cost = rnd.NextDouble() * 50;
            double shared = rnd.NextDouble() * 30;
            double abonum = rnd.NextDouble() * 6000456;
            //when
            bool res = manager.insertAboKostenIntoDb(1, "Testabo" + abonum, cost, shared, DateTime.Now);
            //then
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void getCommentsForDatasetTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            List<Kommentar> res = manager.getCommentsForDataset(1);
            //then
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count != 0);
            Assert.AreNotEqual(res.First().kommentar, "");
        }

        [TestMethod()]
        public void insertCommentForDatasetTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            bool res = manager.insertCommentForDataset(1, "Testkommentar");
            //then
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void deleteCommentForDatasetTest()
        {
            //given
            DatabaseManager manager = new DatabaseManager(true);

            //when
            bool res = manager.insertCommentForDataset(1, "Testkommentar");

            List<Kommentar> kommis = manager.getCommentsForDataset(1);

            bool res1 = manager.deleteCommentForDataset(-1);
            bool res2 = manager.deleteCommentForDataset(kommis.First().kommentarID_PK);
            //then
            Assert.IsTrue(res);
            Assert.IsFalse(res1);
            Assert.IsTrue(res2);
            
        }
    }
}