using SubscriptionService.Domain.Enums;

namespace SubscriptionService.API.ViewModels
{
    public class SubscriptionViewModel
    {
        public Guid Id { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public string Email { get; set; } = string.Empty;
        public Guid FusionUserId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
