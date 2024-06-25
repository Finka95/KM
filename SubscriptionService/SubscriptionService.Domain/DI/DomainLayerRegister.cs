using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.Domain.Interfaces;

namespace SubscriptionService.Domain.DI
{
    public static class DomainLayerRegister
    {
        public static void RegisterDomainDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
