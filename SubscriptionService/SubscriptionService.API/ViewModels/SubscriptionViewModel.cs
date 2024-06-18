using SubscriptionService.Domain.Enums;

namespace SubscriptionService.API.ViewModels
{
    public class SubscriptionViewModel
    {
        public Guid Id { get; set; }
        public SubscriptionType Type { get; set; }
        public Guid FusionUserId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
