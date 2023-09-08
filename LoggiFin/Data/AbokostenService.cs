using LoggiFin.DBO;
using LoggiFin.Global;

namespace LoggiFin.Data
{
    public class AbokostenService
    {

        private DatabaseManager manager = new DatabaseManager(GlobalProperties.debug);

        public List<Abokosten> GetAbokosten()
        {
            return manager.getAbokosten();
        }

    }
}
