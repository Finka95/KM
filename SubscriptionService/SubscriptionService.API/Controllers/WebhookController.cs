using Microsoft.AspNetCore.Mvc;
using SubscriptionService.API.ViewModels;
using SubscriptionService.BLL.Interfaces;
using System.Text.Json.Nodes;
using Mapster;

namespace SubscriptionService.API.Controllers
{
    [Route("api/webhook")]
    [ApiController]
    public class WebhookController
    {
        private readonly ISubscriptionService _subscriptionService;

        public WebhookController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        public async Task<SubscriptionViewModel> Webhook(JsonObject userJson, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionService.CreateSubscriptionAfterUserRegistration(userJson, cancellationToken);
            return subscription.Adapt<SubscriptionViewModel>();
        }
    }
}
