﻿using MassTransit;
using Shared.Events;
using System.Text.Json;
using Tinder.BLL.Interfaces;

namespace Tinder.BLL.MessageBroker
{
    public class SubscriptionExpiredConsumer : IConsumer<SubscriptionExpired>
    {
        private readonly ICacheService _cacheService;

        public SubscriptionExpiredConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public Task Consume(ConsumeContext<SubscriptionExpired> context)
        {
            Console.WriteLine("Message From Subscription is received: " + JsonSerializer.Serialize(context.Message));
            return _cacheService.RemoveAsync(context.Message.Id.ToString());
        }
    }
}
