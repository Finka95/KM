using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NotificationService.BLL.Interfaces;
using NotificationService.BLL.MessageBroker;
using NotificationService.BLL.MessageBroker.Interfaces;
using NotificationService.BLL.Models;
using NotificationService.BLL.Services;

namespace NotificationService.BLL.DI
{
    public static class BusinessLayerRegister
    {
        public static void RegisterBusinessLogicDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBroker"));
            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
            services.Configure<EmailSettings>(configuration.GetSection("Email"));

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.SetKebabCaseEndpointNameFormatter();

                busConfiguration.UsingRabbitMq((context, cfg) =>
                {
                    var settings = context.GetRequiredService<MessageBrokerSettings>();

                    cfg.Host(new Uri(settings.Host), hostConfigure =>
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
