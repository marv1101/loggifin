using System.Text.Json.Serialization;

namespace LoggiFin.DBO
{
    public class JsonDatarow
    {

        [JsonPropertyName("RowId")]
        public int RowId { get; set; }

        [JsonPropertyName("Date")]
        public string DateString { get; set; }

        [JsonPropertyName("Einnahmen")]
        public Decimal Einnahmen { get; set; }

        [JsonPropertyName("Ausgaben")]
        public Decimal Ausgaben { get; set; }

        [JsonPropertyName("Kontostand")]
        public double Kontostand { get; set; }

        [JsonPropertyName("Sparkontostand")]
        public double Sparkontostand { get; set; }

        [JsonPropertyName("VolksbankSparplan")]
        public double VolksbankSparplan { get; set; }

        [JsonPropertyName("ScalableSparplan")]
        public double ScalableSparplan { get; set; }

        [JsonPropertyName("VolkbankSparplanTotal")]
        public double VolksbankSparplanTotal { get; set; }

        [JsonPropertyName("ScalableSparplanTotal")]
        public double ScalableSparplanTotal { get; set; }

        [JsonPropertyName("Beschreibung")]
        public string Beschreibung { get; set; }

        [JsonPropertyName("TransferMenge")]
        public double TransferMenge { get; set; }

        [JsonPropertyName("Bargeld")]
        public double Bargeld { get; set; }

        [JsonPropertyName("Urlaubskasse")]
        public double Urlaubskasse { get; set; }

        public DateOnly? date;

        public string getProfitsAsEuro()
        {
            return (Math.Round(this.Einnahmen - this.Ausgaben), 2) + "€";
        }

        public double getTotalBalance()
        {
            return Kontostand + Sparkontostand + VolksbankSparplan + ScalableSparplan + Bargeld + Urlaubskasse;
        }

        public DateOnly GetDate() {
            DateOnly toRet = new DateOnly();
            bool succ = DateOnly.TryParse(DateString, out toRet);

            if (succ)
            {
                return toRet;
            }

            return DateOnly.MinValue;
        }

        public string ToString()
        {
            return RowId + ", " + date + ", " + Einnahmen.ToString().Replace(",", "") + ", " + Ausgaben.ToString().Replace(",", "") + ", " + Kontostand.ToString().Replace(",", "") + ", " + Sparkontostand.ToString().Replace(",", "") + ", "
                + ScalableSparplan.ToString().Replace(",", "") + ", " + ScalableSparplanTotal.ToString().Replace(",", "") + ", " + VolksbankSparplan.ToString().Replace(",", "") + ", " + VolksbankSparplanTotal.ToString().Replace(",", "") + ", "
                + Beschreibung + ", " + TransferMenge.ToString().Replace(",", "") + ", " + Bargeld.ToString().Replace(",", "") + ", " + Urlaubskasse.ToString().Replace(",", "") + ", " + getTotalBalance().ToString().Replace(",", "");
        }

    }
}
