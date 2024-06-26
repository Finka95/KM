using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.BLL.Interfaces;
using SubscriptionService.DAL.DI;

namespace SubscriptionService.BLL.DI
{
    public static class BusinessLayerRegister
    {s
        public static void RegisterBusinessLogicDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ISubscriptionService, Services.SubscriptionService>();
            services.RegisterDataAccessDependencies(configuration);
        }
    }
}
