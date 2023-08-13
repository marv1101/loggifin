using LoggiFin.DBO;

namespace LoggiFin.Data
{
    public class FinLoggingDataRow
    {
        public int rowId { get; set; }
        public DateOnly date { get; set; }

        //kontodaten eigenschaften
        public double einnahmen { get; set; }
        public double ausgaben { get; set; }
        public double kontostand { get; set; }
        public double sparkontostand { get; set; }

        //investdaten eigenschaften
        public double volksbankSparplan { get; set; }
        public double scalableSparplan { get; set; }

        //transferdaten eigenschaften
        public string beschreibung { get; set; }
        public double amoutTotal { get; set; }

        //bargelddaten eigenschaften
        public double bargeld { get; set; }
        public double urlaubskasse { get; set; }
        //total
        public double totalBalance {  get; set; }

        public FinLoggingDataRow(int id, DateOnly date, Kontodaten kdt, Investmentdaten idt, Transferdaten tdt, Bargelddaten bdt)
        {
            this.rowId = id;
            this.date = date;
            //kontodaten
            this.einnahmen = kdt.einnahmen;
            this.ausgaben = kdt.ausgaben;
            this.kontostand = kdt.kontostand;
            this.sparkontostand = kdt.sparkontostand;
            //Investdaten
            this.scalableSparplan = idt.scalableStand;
            this.volksbankSparplan = idt.volksbankStand;
            //transferdaten
            this.beschreibung = tdt.beschreibung;
            this.amoutTotal = tdt.amountTotal;
            //bargelddaten
            this.bargeld = bdt.bargeld;
            this.urlaubskasse = bdt.urlaubskasse;

            this.totalBalance = kontostand + sparkontostand + volksbankSparplan + scalableSparplan + bargeld + urlaubskasse;
        }

    }
}
