using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Mapper;
using Tinder.BLL.Services;
using Tinder.DAL.DI;

namespace Tinder.BLL.DI
{
    public static class BusinessLayerRegister
    {
        public static void RegisterBusinessLogicDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataAccessDependencies(configuration);

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}