using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.DAL.Interfaces;
using SubscriptionService.DAL.Repositories;
using SubscriptionService.Domain.DI;

namespace SubscriptionService.DAL.DI
{
    public static class DataLayerRegister
    {
        public static void RegisterDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDomainDependencies(configuration);
            services.Configure<MongoDatabaseConfiguration>(configuration.GetSection("MongoDB"));
            services.AddScoped<IApplicationMongoDbContext, ApplicationMongoDbContext>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        }
    }
}
