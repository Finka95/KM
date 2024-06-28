using MassTransit;
using Shared.Events;

namespace Tinder.BLL.MessageBroker.Consumers
{
    public class SubscriptionUpdatedConsumer : IConsumer<SubscriptionUpdated>
    {
        public Task Consume(ConsumeContext<SubscriptionUpdated> context)
        {
            // logic will be implemented
            return Task.CompletedTask;
        }
    }
}
