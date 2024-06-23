using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Events;
using SubscriptionService.BLL.Interfaces;
using SubscriptionService.BLL.MessageBroker.Interfaces;

namespace SubscriptionService.BLL.Services
{
    public class CheckIsSubscriptionExpiredBackgroundService : BackgroundService
    {
        private readonly TimeSpan _period;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CheckIsSubscriptionExpiredBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _period = TimeSpan.FromDays(1);
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var periodicTimer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested && await periodicTimer.WaitForNextTickAsync(stoppingToken))
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var subscriptionService = scope.ServiceProvider.GetRequiredService<ISubscriptionService>();
                var subscriptions = await subscriptionService.GetAllAsync(stoppingToken);
           
                foreach (var subscription in subscriptions)
                {
                    if (DateTime.Now.Date == subscription.ExpiresAt.Date)
                    {
                        var subscriptionExpired = subscription.Adapt<SubscriptionExpired>();
                        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
                        await eventBus.PublishAsync(subscriptionExpired, stoppingToken);
                    }
                }
            }

        }
    }
}
