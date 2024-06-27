using System.Text.Json;
using MassTransit;
using Tinder.BLL.MessageBroker.Interfaces;

namespace Tinder.BLL.MessageBroker
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
            Console.WriteLine("Message is sent: " + msg);
            return _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
