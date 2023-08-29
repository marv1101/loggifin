using LoggiFin.DBO;
using System.Data.SqlClient;
using Insight.Database;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace LoggiFin.Data
{
    public class DatabaseManager
    {

        String connectionString = "";
        MySqlConnectionStringBuilder db;
        DbConnection connection;
        private int[] lastIndic;
        

        public DatabaseManager(bool testingMode) {

            if (testingMode)
            {
                connectionString = "server=10.69.1.40;uid=LoggiFin;" +
                "pwd=loggi!Marian-Fin;database=loggifinTest";
            }
            else
            {
                connectionString = "server=10.69.1.40;uid=ReadWrite;" +
                "pwd=marian;database=loggifin";
            }
            

            db = new MySqlConnectionStringBuilder(connectionString);
            connection = db.Connection();
            connection.Open();
        }

        public List<Finanzdaten> getData()
        {
            List<Finanzdaten> res = (List<Finanzdaten>) connection.QuerySql<Finanzdaten>("SELECT * FROM finanzdaten");

            return res;
        }

        public Kontodaten getKontodaten(int kontodatenID_FK)
        {
            Kontodaten res = ((List<Kontodaten>)connection.QuerySql<Kontodaten>("SELECT * FROM kontodaten WHERE kontodatenID_PK = " + kontodatenID_FK)).First();

            return res;
        }

        public Investmentdaten getInvestdaten(int investdatenID_PK)
        {
            Investmentdaten res = ((List<Investmentdaten>)connection.QuerySql<Investmentdaten>("SELECT * FROM investmentdaten WHERE investdatenID_PK = " + investdatenID_PK)).First();

            return res;
        }

        public Transferdaten getTransferdaten(int transferdatenID_PK)
        {
            Transferdaten res = ((List<Transferdaten>)connection.QuerySql<Transferdaten>("SELECT * FROM transferdaten WHERE transferdatenID_PK = " + transferdatenID_PK)).First();

            return res;
        }

        public Bargelddaten getBargelddaten(int bargelddatenID_PK)
        {
            Bargelddaten res = ((List<Bargelddaten>)connection.QuerySql<Bargelddaten>("SELECT * FROM bargelddaten WHERE bargelddatenID_PK = " + bargelddatenID_PK)).First();

            return res;
        }

        private int[] getLastIndices()
        {
            int[] res = new int[4];

            res[0] = (int)connection.QuerySql<int>("SELECT kontodatenID_PK FROM kontodaten ORDER BY kontodatenID_PK DESC LIMIT 1").First();
            res[1] = (int)connection.QuerySql<int>("SELECT investdatenID_PK FROM investmentdaten ORDER BY investdatenID_PK DESC LIMIT 1").First();
            res[2] = (int)connection.QuerySql<int>("SELECT transferdatenID_PK FROM transferdaten ORDER BY transferdatenID_PK DESC LIMIT 1").First();
            res[3] = (int)connection.QuerySql<int>("SELECT bargelddatenID_PK FROM bargelddaten ORDER BY bargelddatenID_PK DESC LIMIT 1").First();

            return res;
        }

        public bool insertDataIntoDb(DateOnly date, double einnahmen, double ausgaben, double kontostand, double sparkonto,
            double scalableSparplan, double volksbankSparplan, string beschreibung, double totalAmount, double bargeld, double urlaubskasse)
        {

            int res = connection.ExecuteSql("INSERT INTO investmentdaten VALUES(DEFAULT, " + volksbankSparplan.ToString().Replace(",", ".") + ", " + scalableSparplan.ToString().Replace(",", ".") + ")");
            if (res == 0) return false;

            res = connection.ExecuteSql("INSERT INTO kontodaten VALUES(DEFAULT, " + einnahmen.ToString().Replace(",", ".") + ", " + ausgaben.ToString().Replace(",", ".") + ", " + kontostand.ToString().Replace(",", ".") + ", " + sparkonto.ToString().Replace(",", ".") + ")");
            if (res == 0) return false;

            res = connection.ExecuteSql("INSERT INTO transferdaten VALUES(DEFAULT, \"" + beschreibung.ToString().Replace(",", ".") + "\", " + totalAmount.ToString().Replace(",", ".") + ")");
            if (res == 0) return false;

            res = connection.ExecuteSql("INSERT INTO bargelddaten VALUES(DEFAULT, " + bargeld.ToString().Replace(",", ".") + ", " + urlaubskasse.ToString().Replace(",", ".") + ")");
            if (res == 0) return false;

            lastIndic = getLastIndices();
            res = connection.ExecuteSql("INSERT INTO finanzdaten VALUES(DEFAULT, '" + date.Year + "-" + date.Month + "-" + date.Day + "', " + lastIndic[0] + ", " + lastIndic[1] + ", " + lastIndic[2] + ", " + lastIndic[3] + ")");
            if (res == 0) return false;

            return true;
        }

        public bool authUser(string username, string password)
        {
            string pwd;

            try
            {
                pwd = connection.QuerySql<string>("SELECT passwd FROM users WHERE username='" + username + "'").First();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            if (password.Equals(pwd)) return true;

            return false;
        }

        public bool removeData(int datasetID_PK)
        {

            List<Finanzdaten> res = (List<Finanzdaten>)connection.QuerySql<Finanzdaten>("SELECT * FROM finanzdaten WHERE datasetID_PK=" + datasetID_PK);

            if (res == null) return false;
            if (res.Count == 0) return false;

            Finanzdaten finanzdaten = res.First();

            int kontodatenID_FK = finanzdaten.kontodatenID_FK;
            int investmentdatenID_FK = finanzdaten.investdatenID_FK;
            int transferdatenID_FK = finanzdaten.transferdatenID_FK;
            int bargelddatenID_FK = finanzdaten.bargelddatenID_FK;


            int deleted = 0;

            try
            {
                deleted = connection.ExecuteSql("DELETE FROM finanzdaten WHERE datasetID_PK = " + datasetID_PK);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            
            if(deleted != 0)
            {
                deleted = connection.ExecuteSql("DELETE FROM bargelddaten WHERE bargelddatenID_PK = " + bargelddatenID_FK);
                if (deleted == 0) return false;
                deleted = connection.ExecuteSql("DELETE FROM investmentdaten WHERE investdatenID_PK = " + investmentdatenID_FK);
                if (deleted == 0) return false;
                deleted = connection.ExecuteSql("DELETE FROM kontodaten WHERE kontodatenID_PK = " + kontodatenID_FK);
                if (deleted == 0) return false;
                deleted = connection.ExecuteSql("DELETE FROM transferdaten WHERE transferdatenID_PK = " + transferdatenID_FK);
                if (deleted == 0) return false;
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}
