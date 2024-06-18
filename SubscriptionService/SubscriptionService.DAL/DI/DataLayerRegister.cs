using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubscriptionService.DAL.Interfaces;
using SubscriptionService.DAL.Repositories;

namespace SubscriptionService.DAL.DI
{
    public static class DataLayerRegister
    {
        public static void RegisterDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDatabaseConfiguration>(configuration.GetSection("MongoDB"));
            services.AddScoped<IApplicationMongoDbContext, ApplicationMongoDbContext>();
        }
    }
}
