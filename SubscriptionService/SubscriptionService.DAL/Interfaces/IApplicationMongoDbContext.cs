using MongoDB.Driver;

namespace SubscriptionService.DAL.Interfaces
{
    public interface IApplicationMongoDbContext
    {
        public IMongoCollection<T> GetCollection<T>(string name);
    }
}
