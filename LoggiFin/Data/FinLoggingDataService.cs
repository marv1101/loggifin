using LoggiFin.DBO;
using LoggiFin.Global;

namespace LoggiFin.Data
{
    public class FinLoggingDataService
    {

        private int rownum = -1;
        DatabaseManager dbm;
        IEnumerable<FinLoggingDataRow> dataRows;

        public FinLoggingDataService()
        {
            dbm = new DatabaseManager(GlobalProperties.debug);
            List<Finanzdaten> finanzdaten = dbm.getData();

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

    }
}
