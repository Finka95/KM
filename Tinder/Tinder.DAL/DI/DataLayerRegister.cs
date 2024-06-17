using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tinder.DAL.Repositories;
using Tinder.DAL.Interfaces;

namespace Tinder.DAL.DI;

public static class DataLayerRegister
{
    public static void RegisterDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(option => 
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
    }
}
