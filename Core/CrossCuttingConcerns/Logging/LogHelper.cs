using Serilog;

namespace Core.CrossCuttingConcerns.Logging
{
    public static class LogHelper
    {
        public static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Debug()
                .CreateLogger();
        }
    }
}
