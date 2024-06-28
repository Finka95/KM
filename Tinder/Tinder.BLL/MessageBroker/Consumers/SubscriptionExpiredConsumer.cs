using MassTransit;
using Shared.Events;
using Tinder.BLL.Interfaces;

namespace Tinder.BLL.MessageBroker.Consumers
{
    public class SubscriptionExpiredConsumer : IConsumer<SubscriptionExpired>
    {
        private readonly ICacheService _cacheService;

        public SubscriptionExpiredConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Task Consume(ConsumeContext<SubscriptionExpired> context)
        {
            return _cacheService.RemoveAsync(context.Message.Id.ToString(), default);
        }
    }
}
