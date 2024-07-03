using GraphQlService.BLL.Interfaces;
using GraphQlService.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQlService.BLL.DI
{
    public static class BusinessLayerRegister
    {
        public static void RegisterBusinessLogicDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddHttpClient("Tinder", (_, httpClient) =>
            {
                var tinderUrl = configuration["Tinder:Url"];
                httpClient.BaseAddress = new Uri(tinderUrl);
            });
        }
    }
}