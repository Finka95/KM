using GraphQlService.BLL;

namespace GraphQlService.ViewModels
{
    public class SubscriptionViewModel
    {
        public Guid Id { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
