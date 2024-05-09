using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tinder.DAL.DI;

public static class DataLayerRegister
{
    public static void RegisterDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(option => 
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}
