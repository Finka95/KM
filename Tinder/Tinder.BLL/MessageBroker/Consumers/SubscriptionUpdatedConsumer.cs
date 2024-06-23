using MassTransit;
using Shared.Events;
using Tinder.BLL.Interfaces;

namespace Tinder.BLL.MessageBroker.Consumers
{
    public class SubscriptionUpdatedConsumer : IConsumer<SubscriptionUpdated>
    {
        private readonly ICacheService _cacheService;

        public SubscriptionUpdatedConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public Task Consume(ConsumeContext<SubscriptionUpdated> context)
        {
            return _cacheService.SetAsync(context.Message.Id.ToString(), context.Message);
        }
    }
}
