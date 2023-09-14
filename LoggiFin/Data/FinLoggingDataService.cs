using LoggiFin.DBO;
using LoggiFin.Global;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoggiFin.Data
{
    public class FinLoggingDataService
    {

        private int rownum = -1;
        private static DatabaseManager dbm;
        IEnumerable<FinLoggingDataRow> dataRows;
        List<Abokosten> abokosten;

        public FinLoggingDataService()
        {
            if(dbm == null)
            {
                dbm = new DatabaseManager(GlobalProperties.debug);
            }
            List<Finanzdaten> finanzdaten = dbm.getData();
            abokosten = dbm.getAbokosten();

            List<FinLoggingDataRow> data = new List<FinLoggingDataRow>();
            Kontodaten kdt;
            Investmentdaten idt;
            Transferdaten tdt;
            Bargelddaten bdt;

            foreach(Finanzdaten fin in finanzdaten)
            {
                //staing same
                int id = fin.datasetID_PK;
                DateOnly date = DateOnly.FromDateTime(fin.entryDate);

                //to process
                int kontodatenID = fin.kontodatenID_FK;
                //kontodaten processing
                kdt = dbm.getKontodaten(kontodatenID);

                //investdaten processing
                int investDatenID = fin.investdatenID_FK;
                idt = dbm.getInvestdaten(investDatenID);

                //transferdaten processing
                int transferDatenID = fin.transferdatenID_FK;
                tdt = dbm.getTransferdaten(transferDatenID);

                //bargelddaten processing
                int bargeldDatenID = fin.bargelddatenID_FK;
                bdt = dbm.getBargelddaten(bargeldDatenID);

                data.Add(new FinLoggingDataRow(id, date, kdt, idt, tdt, bdt));

            }

            dataRows = data;
        }

        public IEnumerable<FinLoggingDataRow> GetDataRows()
        {
            return this.dataRows;
        }

        public List<Abokosten> GetAbokosten()
        {
            return this.abokosten;
        }

        public static string getTransferText(int datasetID)
        {
            string res = "";

            if(dbm == null)
            {
                dbm = new DatabaseManager(GlobalProperties.debug);
            }

            List<Finanzdaten> finanzdaten = dbm.getData();

            foreach (Finanzdaten fin in finanzdaten)
            {

                //transferdaten processing
                if(fin.datasetID_PK == datasetID)
                {
                    int transferDatenID = fin.transferdatenID_FK;
                    res += dbm.getTransferdaten(transferDatenID).beschreibung;
                }
            }

            return res;
        }

    }
}
