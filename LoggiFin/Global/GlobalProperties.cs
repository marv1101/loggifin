namespace LoggiFin.Global
{
    public static class GlobalProperties
    {

        public static bool debug = true;
        public static int id = 1;

        public static void readFromEnv()
        {
        
            String? confEnv = Environment.GetEnvironmentVariable("confEnv");
            if (confEnv != null)
            {
                String? getDebug = Environment.GetEnvironmentVariable ("debug");
                Console.WriteLine(getDebug);
                if(getDebug != null)
                {
                    debug = getDebug.Equals ("true");
                }
            }
        }
    }
}