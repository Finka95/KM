using SubscriptionService.BLL.Models;
using System.Text.Json.Nodes;

namespace SubscriptionService.BLL.Interfaces
{
    public interface ISubscriptionService : IGenericService<Subscription>
    {
        public Task<Subscription> CreateSubscriptionAfterUserRegistration(JsonObject request, CancellationToken cancellationToken);
    }
}
