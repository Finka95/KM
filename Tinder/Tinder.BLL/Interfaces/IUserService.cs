using System.Text.Json.Nodes;
using Tinder.BLL.Models;

namespace Tinder.BLL.Interfaces
{
    public interface IUserService : IGenericService<User>
    {
        public Task<User> CreateUserFromJson(JsonObject userJson, CancellationToken cancellationToken);
        public Task<User> SetSubscriptionIdAsync(Guid fusionUserId, Guid subscriptionId, CancellationToken cancellationToken);
    }
}
