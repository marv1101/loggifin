using System.Text.Json.Serialization;

namespace LoggiFin.DBO
{
    public class Kommentar
    {
        [JsonPropertyName("id")]
        public int kommentarID_PK { get; set; }
        [JsonPropertyName("comment")]
        public string kommentar {  get; set; }
        public int datasetID_FK { get; set; }

    }
}
