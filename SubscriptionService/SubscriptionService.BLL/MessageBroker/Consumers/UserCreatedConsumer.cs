using Mapster;
using MassTransit;
using Shared.Events;
using SubscriptionService.BLL.Interfaces;
using SubscriptionService.BLL.MessageBroker.Interfaces;
using SubscriptionService.BLL.Models;

namespace SubscriptionService.BLL.MessageBroker.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IEventBus _eventBus;

        public UserCreatedConsumer(ISubscriptionService subscriptionService,
            IEventBus eventBus)
        {
            _subscriptionService = subscriptionService;
            _eventBus = eventBus;
        }
        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var subscriptionToCreate = context.Message.Adapt<Subscription>();
            var createdSubscription = await _subscriptionService.CreateAsync(subscriptionToCreate.FusionUserId,
                subscriptionToCreate, default);
        }
    }
}
