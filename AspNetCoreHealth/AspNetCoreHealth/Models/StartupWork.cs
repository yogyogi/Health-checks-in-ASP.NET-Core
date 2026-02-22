namespace AspNetCoreHealth.Models
{
    public class StartupWork
    {
        private static readonly DateTime _startTime = DateTime.UtcNow;
        public static bool DoWork()
        {
            if ((DateTime.UtcNow - _startTime).TotalSeconds >= 10)
            {
                return true;
            }

            return false;
        }
    }
}
