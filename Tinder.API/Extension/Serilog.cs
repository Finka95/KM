using Serilog;

namespace Tinder.API.Extension
{
    public static class Serilog
    {
       public static IHostBuilder AddSerilogConfiguration(this IHostBuilder builder)
       {
           return builder.ConfigureServices((hostingContext, services) => {
              Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Information()
                  .WriteTo.Console()
                  .WriteTo.File("logs/logs.txt", rollingInterval:RollingInterval.Day)
                  .CreateLogger();
              
               services.AddSerilog();
           });
       }
    }
}
