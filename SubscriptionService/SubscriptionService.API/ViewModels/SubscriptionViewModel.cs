namespace SubscriptionService.API.ViewModels
{
    public class SubscriptionViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
