using Insight.Database.Reliable;
using LoggiFin.Data;
using LoggiFin.DBO;
using System.IO;
using System.Text;

namespace LoggiFin.Services
{
    public class CSVExporter
    {

        private IEnumerable<JsonDatarow> data;
        private String path = "export.csv";

        public CSVExporter(List<JsonDatarow> toExport) {

            data = toExport;
        
        }

        public Boolean createFile()
        {
            if (data == null)
            {
                return false;
            }

            FileInfo file = new FileInfo("export.csv");
            string fullFile = file.FullName;

            string header = "rowId , date, einnahmen, ausgaben, kontostand, sparkontostand, "
                + "scalableSparplan, scalableSparplanTotal, volksbankSparplan, volksbankSparplanTotal, "
                + "beschreibung, amoutTotal, bargeld, urlaubskasse, totalBalance\n";

            // Create the file, or overwrite if the file exists.
            using (FileStream fs = File.Create(fullFile))
            {
                byte[] info = new ASCIIEncoding().GetBytes(header);
                fs.Write(info, 0, info.Length);
                foreach (JsonDatarow row in data)
                {
                    info = new ASCIIEncoding().GetBytes(row.ToString().Replace("€", "¤") + "\n");
                    fs.Write(info, 0, info.Length);
                }
                
            }

            return true;
        }

        public Stream getFileStreamForDownload()
        {
            if (data == null)
            {
                return Stream.Null;
            }

            FileInfo file = new FileInfo("export.csv");
            
            bool res = createFile();

            if(res)
            {
                return File.OpenRead(file.Name);
            }
            else
            {
                return Stream.Null;
            }
            
        }

    }
}
