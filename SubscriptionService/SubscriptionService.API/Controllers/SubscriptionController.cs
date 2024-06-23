using Mapster;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.API.ViewModels;
using SubscriptionService.BLL.Interfaces;
using SubscriptionService.BLL.Models;
using SubscriptionService.Domain.Enums;

namespace SubscriptionService.API.Controllers
{
    [Route("api/subscriptions")]
    [ApiController]
    public class SubscriptionController : ControllerBase
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

        [HttpPost("{fusionUserId}")]
        public async Task<SubscriptionViewModel> CreateAsync(Guid fusionUserId, SubscriptionCreationViewModel subscriptionCreationViewModel, CancellationToken cancellationToken)
        {
            var subscriptionToCreate = subscriptionCreationViewModel.Adapt<Subscription>();
            var createdSubscription = await _subscriptionService.CreateAsync(fusionUserId, subscriptionToCreate, cancellationToken);
            return createdSubscription.Adapt<SubscriptionViewModel>();
        }
        
        [HttpDelete("{id}")]
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _subscriptionService.DeleteAsync(id, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<SubscriptionViewModel> UpdateAsync(Guid id, SubscriptionType subscriptionType, CancellationToken cancellationToken)
        {
            var subscriptionModel = await _subscriptionService.UpdateAsync(id, subscriptionType, cancellationToken);
            return subscriptionModel.Adapt<SubscriptionViewModel>();
        }
    }
}