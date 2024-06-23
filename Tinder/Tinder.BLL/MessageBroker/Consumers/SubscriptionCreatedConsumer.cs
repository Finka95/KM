using MassTransit;
using Shared.Events;
using System.Text.Json;
using Tinder.BLL.Interfaces;

namespace Tinder.BLL.MessageBroker.Consumers
{
    public class SubscriptionCreatedConsumer : IConsumer<SubscriptionCreated>
    {
        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;

        public SubscriptionCreatedConsumer(IUserService userService, ICacheService cacheService)
        {
            _userService = userService;
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<SubscriptionCreated> context)
        {
            Console.WriteLine("Message From Subscription is received: " + JsonSerializer.Serialize(context.Message));
            await _cacheService.SetAsync(context.Message.Id.ToString(), context.Message);
            await _userService.SetSubscriptionIdAsync(context.Message.FusionUserId, context.Message.Id, default);
        }
    }
}
