using SubscriptionService.Domain.Enums;

namespace SubscriptionService.API.ViewModels
{
    public class SubscriptionCreationViewModel
    {
        public Guid UserId { get; set; }
        public SubscriptionType Type { get; set; }
    }
}
