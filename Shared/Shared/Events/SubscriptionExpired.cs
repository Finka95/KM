﻿namespace Shared.Events
{
    public class SubscriptionExpired
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
