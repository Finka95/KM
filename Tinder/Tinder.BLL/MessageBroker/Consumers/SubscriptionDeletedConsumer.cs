using MassTransit;
using Shared.Events;
using Tinder.BLL.Interfaces;

namespace Tinder.BLL.MessageBroker.Consumers
{
    public class SubscriptionDeletedConsumer : IConsumer<SubscriptionDeleted>
    {
        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;

        public SubscriptionDeletedConsumer(IUserService userService,
            ICacheService cacheService)
        {
            _userService = userService;
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<SubscriptionDeleted> context)
        {
            await _cacheService.RemoveAsync(context.Message.Id.ToString(), default);
            await _userService.SetSubscriptionIdAsync(context.Message.FusionUserId, 
                Guid.Empty, default);
        }
    }
}
