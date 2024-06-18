using Mapster;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.API.ViewModels;
using SubscriptionService.BLL.Interfaces;
using SubscriptionService.BLL.Models;

namespace SubscriptionService.API.Controllers
{
    [Route("api/subscriptions")]
    [ApiController]
    public class SubscriptionController
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<List<SubscriptionViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var subscriptionModels = await _subscriptionService.GetAllAsync(cancellationToken);
            var subscriptionViewModels = subscriptionModels.Adapt<List<SubscriptionViewModel>>();
            return subscriptionViewModels;
        }

        [HttpGet("{id}")]
        public async Task<SubscriptionViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var subscriptionModel = await _subscriptionService.GetByIdAsync(id, cancellationToken);
            return subscriptionModel.Adapt<SubscriptionViewModel>();
        }

        [HttpPost]
        public async Task<SubscriptionViewModel> CreateAsync(SubscriptionCreationViewModel subscriptionCreationViewModel, CancellationToken cancellationToken)
        {
            var subscriptionToCreate = subscriptionCreationViewModel.Adapt<Subscription>();
            var createdSubscription = await _subscriptionService.CreateAsync(subscriptionToCreate, cancellationToken);
            return createdSubscription.Adapt<SubscriptionViewModel>();
        }
        
        [HttpDelete]
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _subscriptionService.DeleteAsync(id, cancellationToken);
        }
    }
}