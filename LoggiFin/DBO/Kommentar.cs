namespace LoggiFin.DBO
{
    public class Kommentar
    {

        public int kommentarID_PK { get; set; }
        public string kommentar {  get; set; }
        public int datasetID_FK { get; set; }

    }
}
