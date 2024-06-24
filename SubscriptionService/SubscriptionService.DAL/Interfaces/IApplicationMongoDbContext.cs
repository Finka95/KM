using MongoDB.Driver;
using SubscriptionService.DAL.Entities;

namespace SubscriptionService.DAL.Interfaces
{
    public interface IApplicationMongoDbContext
    {
        public IMongoCollection<SubscriptionEntity> GetCollection();
    }
}
