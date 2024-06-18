using SubscriptionService.Domain.Enums;

namespace SubscriptionService.API.ViewModels
{
    public class SubscriptionCreationViewModel
    {
        public Guid FusionUserId { get; set; }
        public SubscriptionType Type { get; set; }
    }
}
