using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tinder.DAL.DI;

namespace Tinder.BLL.DI
{
    public static class BusinessLayerRegister
    {
        public static void RegisterBusinessLogicDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataAccessDependencies(configuration);
        }
    }
}