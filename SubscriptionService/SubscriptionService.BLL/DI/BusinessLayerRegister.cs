using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SubscriptionService.BLL.Interfaces;
using SubscriptionService.BLL.MessageBroker;
using SubscriptionService.BLL.MessageBroker.Interfaces;
using SubscriptionService.BLL.Services;
using SubscriptionService.DAL.DI;

namespace SubscriptionService.BLL.DI
{
    public static class BusinessLayerRegister
    {
        public static void RegisterBusinessLogicDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataAccessDependencies(configuration);
            services.AddScoped<ISubscriptionService, Services.SubscriptionService>();
            services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBroker"));
            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.SetKebabCaseEndpointNameFormatter();

                busConfiguration.AddConsumer<UserCreatedConsumer>();
                busConfiguration.UsingRabbitMq((context, cfg) =>
                {
                    var settings = context.GetRequiredService<MessageBrokerSettings>();
                    cfg.Host("localhost", hostConfigure =>
                    {
                        hostConfigure.Username(settings.Username);
                        hostConfigure.Password(settings.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddScoped<IEventBus, EventBus>();
        }
    }
}
