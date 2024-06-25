namespace SubscriptionService.Domain.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime GetUtcNow { get; }
    }
}
