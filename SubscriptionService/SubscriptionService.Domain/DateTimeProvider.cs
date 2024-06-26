using SubscriptionService.Domain.Interfaces;

namespace SubscriptionService.Domain
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
