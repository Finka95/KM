using Shared.Enums;

namespace Tinder.BLL.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public Guid FusionUserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
