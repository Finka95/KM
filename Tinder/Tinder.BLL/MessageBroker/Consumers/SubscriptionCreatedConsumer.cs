using MassTransit;
using Shared.Events;
using System.Text.Json;
using Tinder.BLL.Interfaces;

namespace Tinder.BLL.MessageBroker.Consumers
{
    public class SubscriptionCreatedConsumer : IConsumer<SubscriptionCreated>
    {
        private readonly IUserService _userService;

        public SubscriptionCreatedConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public Task Consume(ConsumeContext<SubscriptionCreated> context)
        {
            return _userService.SetSubscriptionIdAsync(context.Message.FusionUserId, context.Message.Id, default);
        }
    }
}
