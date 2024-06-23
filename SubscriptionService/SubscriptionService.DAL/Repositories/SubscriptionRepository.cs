using MongoDB.Driver;
using SubscriptionService.DAL.Entities;
using SubscriptionService.DAL.Interfaces;

namespace SubscriptionService.DAL.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IMongoCollection<SubscriptionEntity> _subscriptionCollection;

        public SubscriptionRepository(IApplicationMongoDbContext context)
        {
            _subscriptionCollection = context.GetCollection();
        }

        public async Task<SubscriptionEntity> CreateAsync(SubscriptionEntity subscriptionEntity, CancellationToken cancellationToken)
        {
            await _subscriptionCollection.InsertOneAsync(subscriptionEntity, null, cancellationToken);
            return subscriptionEntity;
        }
        
        public Task DeleteAsync(SubscriptionEntity subscriptionEntity, CancellationToken cancellationToken)
        {
            var filterBuilder = new FilterDefinitionBuilder<SubscriptionEntity>();
            var filter = filterBuilder.Where(e => e.Id == subscriptionEntity.Id);
            return _subscriptionCollection.DeleteOneAsync(filter, cancellationToken);
        }

        public Task<List<SubscriptionEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            var filter = new FilterDefinitionBuilder<SubscriptionEntity>().Empty;
            return _subscriptionCollection.Find(filter).ToListAsync(cancellationToken);
        }

        public Task<SubscriptionEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _subscriptionCollection.Find(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<SubscriptionEntity> GetByFusionUserIdAsync(Guid fusionUserId, CancellationToken cancellationToken)
        {
            return _subscriptionCollection.Find(e => e.FusionUserId == fusionUserId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAsync(Guid id, SubscriptionEntity subscriptionEntity, CancellationToken cancellationToken)
        {
            var filter = new FilterDefinitionBuilder<SubscriptionEntity>().Where(s => s.Id == id);
            var updateDefinition = new UpdateDefinitionBuilder<SubscriptionEntity>()
                .Set(s => s.Id, subscriptionEntity.Id)
                .Set(s => s.FusionUserId, subscriptionEntity.FusionUserId)
                .Set(s => s.UpdatedAt, DateTime.Now)
                .Set(s => s.CreatedAt, subscriptionEntity.CreatedAt)
                .Set(s => s.ExpiresAt, DateTime.Now.AddMonths(1))
                .Set(s => s.SubscriptionType, subscriptionEntity.SubscriptionType);
           await _subscriptionCollection.UpdateOneAsync(filter, updateDefinition, null, cancellationToken);
        }
    }
}
