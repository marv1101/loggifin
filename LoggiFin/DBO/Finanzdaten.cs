namespace LoggiFin.DBO
{
    public class Finanzdaten
    {

        public int datasetID_PK { get; set; }
        public int kontodatenID_FK { get; set; }
        public int investdatenID_FK { get; set; }
        public int transferdatenID_FK { get; set; }
        public int bargelddatenID_FK { get; set; }
        public DateTime entryDate { get; set; }

    }
}
