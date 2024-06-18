using SubscriptionService.DAL.Entities;

namespace SubscriptionService.DAL.Interfaces
{
    public interface ISubscriptionRepository
    {
        public Task<List<SubscriptionEntity>> GetAllAsync(CancellationToken cancellationToken);
        public Task<SubscriptionEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<SubscriptionEntity> CreateAsync(SubscriptionEntity subscriptionEntity,
            CancellationToken cancellationToken);
        public Task DeleteAsync(SubscriptionEntity subscriptionEntity,
            CancellationToken cancellationToken);
    }
}
