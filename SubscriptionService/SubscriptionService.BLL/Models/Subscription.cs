﻿using SubscriptionService.Domain.Enums;

namespace SubscriptionService.BLL.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public Guid FusionUserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
