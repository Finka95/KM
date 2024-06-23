using MassTransit;
using Shared.Events;
using Tinder.BLL.Interfaces;

namespace Tinder.BLL.MessageBroker.Consumers
{
    public class SubscriptionDeletedConsumer : IConsumer<SubscriptionDeleted>
    {
        private readonly ICacheService _cacheService;
        private readonly IUserService _userService;

        public SubscriptionDeletedConsumer(ICacheService cacheService, IUserService userService)
        {
            _cacheService = cacheService;
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<SubscriptionDeleted> context)
        {
            await _userService.SetSubscriptionIdAsync(context.Message.FusionUserId, Guid.Empty, default);
            await _cacheService.RemoveAsync(context.Message.Id.ToString());
        }
    }
}
