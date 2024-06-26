using SubscriptionService.Domain.Enums;

namespace SubscriptionService.API.ViewModels
{
    public class SubscriptionCreationViewModel
    {
        public SubscriptionType SubscriptionType { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
