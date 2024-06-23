using Shared.Enums;

namespace Shared.Events
{
    public class SubscriptionUpdated
    {
        public Guid Id { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public Guid FusionUserId { get; set; }
    }
}
