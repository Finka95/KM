using Serilog;

namespace Tinder.API.Extension
{
    public static class Serilog
    {
        public static IServiceCollection Add(
            this IServiceCollection services,
            LoggerConfiguration configuration)
        {
            Log.Logger = configuration.CreateLogger();

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            return services;
        }
    }
}
