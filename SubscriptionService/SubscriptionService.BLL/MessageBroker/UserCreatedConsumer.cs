using Mapster;
using MassTransit;
using Shared.Events;
using SubscriptionService.DAL.Interfaces;

namespace SubscriptionService.BLL.MessageBroker
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public UserCreatedConsumer(IPublishEndpoint publishEndpoint, ISubscriptionRepository subscriptionRepository)
        {
            _publishEndpoint = publishEndpoint;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var subscriptionFromRepo = await _subscriptionRepository.GetByFusionUserIdAsync(context.Message.FusionUserId, default);
            var subscription = subscriptionFromRepo.Adapt<SubscriptionCreated>();
            await _publishEndpoint.Publish(subscription);
        }
    }
}
