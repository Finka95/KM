using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SubscriptionService.DAL.Entities;
using SubscriptionService.DAL.Interfaces;

namespace SubscriptionService.DAL
{
    public class ApplicationMongoDbContext : IApplicationMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public ApplicationMongoDbContext(IOptions<MongoDatabaseConfiguration> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _database = mongoClient.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<Entities.SubscriptionEntity> GetCollection()
        {
            return _database.GetCollection<SubscriptionEntity>("Subscriptions");
        }
    }
}
