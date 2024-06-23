using MassTransit;
using SubscriptionService.BLL.MessageBroker.Interfaces;
using System.Text.Json;

namespace SubscriptionService.BLL.MessageBroker
{
    public class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken)
            where T : class
        {
            var msg = JsonSerializer.Serialize(message);
            return _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
