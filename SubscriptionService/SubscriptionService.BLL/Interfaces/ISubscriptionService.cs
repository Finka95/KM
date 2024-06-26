using System.Text.Json.Nodes;
using SubscriptionService.BLL.Models;
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.BLL.Interfaces
{
    public interface ISubscriptionService
    {
        public Task<Subscription> CreateAsync(Guid fusionUserId, Subscription subscription, CancellationToken cancellationToken);
        public Task<Subscription> UpdateAsync(Guid id, SubscriptionType subscriptionType,
            CancellationToken cancellationToken);
        Task<Subscription> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Subscription>> GetAllAsync(CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
