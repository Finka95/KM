using MassTransit;
using Shared.Events;

namespace Tinder.BLL.MessageBroker.Consumers
{
    internal class SubscriptionExpiredConsumer : IConsumer<SubscriptionExpired>
    {
        public Task Consume(ConsumeContext<SubscriptionExpired> context)
        {
            // logic will be implemented
            return Task.CompletedTask;
        }
    }
}
