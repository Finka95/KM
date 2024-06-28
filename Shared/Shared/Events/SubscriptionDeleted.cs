namespace Shared.Events
{
    public class SubscriptionDeleted
    {
        public Guid Id { get; set; }
        public Guid FusionUserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
