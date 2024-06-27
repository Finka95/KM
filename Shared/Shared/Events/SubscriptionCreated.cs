using Shared.Enums;

namespace Shared.Events
{
    public class SubscriptionCreated
    {
        public Guid Id { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public Guid FusionUserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
