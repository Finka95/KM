namespace SubscriptionService.DAL.Entities
{
    public class SubscriptionEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
