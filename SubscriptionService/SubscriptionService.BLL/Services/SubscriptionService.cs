using Mapster;
using SubscriptionService.BLL.Interfaces;
using SubscriptionService.BLL.Models;
using SubscriptionService.DAL.Entities;
using SubscriptionService.DAL.Interfaces;
using Tinder.BLL.Exceptions;

namespace SubscriptionService.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Subscription> CreateAsync(Subscription subscription, CancellationToken cancellationToken)
        {
            var modelToCreate = subscription.Adapt<SubscriptionEntity>();
            var entity = await _subscriptionRepository.CreateAsync(modelToCreate, cancellationToken);
            return entity.Adapt<Subscription>();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var subscriptionEntity = await _subscriptionRepository.GetByIdAsync(id, cancellationToken);
            if (subscriptionEntity is null)
            {
                return;
            }
            await _subscriptionRepository.DeleteAsync(subscriptionEntity, cancellationToken);
        }

        public async Task<List<Subscription>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _subscriptionRepository.GetAllAsync(cancellationToken);
            return entities.Adapt<List<Subscription>>();
        }

        public async Task<Subscription> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _subscriptionRepository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Entity with this id doesn't exist"); ;
            return entity.Adapt<Subscription>();
        }
    }
}
