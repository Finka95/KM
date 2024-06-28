using MassTransit;
using Shared.Events;
using Tinder.BLL.Interfaces;

namespace Tinder.BLL.MessageBroker.Consumers
{
    public class SubscriptionDeletedConsumer : IConsumer<SubscriptionDeleted>
    {
        private readonly IUserService _userService;

        public SubscriptionDeletedConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public Task Consume(ConsumeContext<SubscriptionDeleted> context)
        {
            return _userService.SetSubscriptionIdAsync(context.Message.FusionUserId, 
                Guid.Empty, default);
        }
    }
}
