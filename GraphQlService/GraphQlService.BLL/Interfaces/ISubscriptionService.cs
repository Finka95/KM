using GraphQlService.BLL.Models;

namespace GraphQlService.BLL.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetSubscriptionsAsync();
        Task<Subscription> GetSubscriptionByIdAsync(Guid id);
    }
}
