﻿using MassTransit;
using NotificationService.BLL.MessageBroker.Interfaces;

namespace NotificationService.BLL.MessageBroker
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
            return _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
