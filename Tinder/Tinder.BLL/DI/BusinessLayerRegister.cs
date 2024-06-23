using Microsoft.Extensions.Configuration;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Mapper;
using Tinder.BLL.MessageBroker;
using Tinder.BLL.MessageBroker.Interfaces;
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

            services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBroker"));
            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.SetKebabCaseEndpointNameFormatter();

                busConfiguration.UsingRabbitMq((context, configurator) =>
                {
                    var settings = context.GetRequiredService<MessageBrokerSettings>();
                    configurator.Host(new Uri(settings.Host), hostConfigure =>
                    {
                        hostConfigure.Username(settings.Username);
                        hostConfigure.Password(settings.Password);
                    });

                   configurator.ConfigureEndpoints(context);
                });
            });
            services.AddScoped<IEventBus, EventBus>();
        }
    }
}