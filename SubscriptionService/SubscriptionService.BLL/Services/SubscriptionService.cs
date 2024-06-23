using Mapster;
using Newtonsoft.Json.Linq;
using SubscriptionService.BLL.Interfaces;
using SubscriptionService.BLL.Models;
using SubscriptionService.DAL.Entities;
using SubscriptionService.DAL.Interfaces;
using System.Text.Json.Nodes;
using SubscriptionService.Domain.Enums;
using SubscriptionService.Domain.Exceptions;
using Shared.Events;
using SubscriptionService.BLL.MessageBroker.Interfaces;

namespace SubscriptionService.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IEventBus _eventBus;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IEventBus eventBus)
        {
            _subscriptionRepository = subscriptionRepository;
            _eventBus = eventBus;
        }

        public async Task<Subscription> CreateAsync(Guid fusionUserId, Subscription subscription, CancellationToken cancellationToken)
        { 
            var userSubscription = await _subscriptionRepository.GetByFusionUserIdAsync(fusionUserId, cancellationToken);
            if (userSubscription.Id == Guid.Empty)
            {
                throw new BadRequestException("User already has subscription");
            }
            
            var modelToCreate = new SubscriptionEntity
            {
               FusionUserId = fusionUserId,
               SubscriptionType = subscription.SubscriptionType,
               CreatedAt = DateTime.Now,
               UpdatedAt = DateTime.Now,
               ExpiresAt = DateTime.Now.AddMonths(1)
            };
            var entity = await _subscriptionRepository.CreateAsync(modelToCreate, cancellationToken);
            var subscriptionCreated = entity.Adapt<SubscriptionCreated>();
            await _eventBus.PublishAsync(subscriptionCreated, cancellationToken);
            return entity.Adapt<Subscription>();
        }

        public async Task<Subscription> CreateSubscriptionAfterUserRegistration(JsonObject request, CancellationToken cancellationToken)
        {
            var jsonObject = JObject.Parse(request.ToJsonString());
            var userJson = jsonObject["event"]["user"];

            var fusionUserId = Guid.Parse(userJson["id"].ToString());

            var subscription = new Subscription
            {
                FusionUserId = fusionUserId,
                SubscriptionType = SubscriptionType.Base,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMonths(1)
            };

            var subscriptionEntity = subscription.Adapt<SubscriptionEntity>();
            var createdSubscription = await _subscriptionRepository.CreateAsync(subscriptionEntity, cancellationToken);
            return createdSubscription.Adapt<Subscription>();
        }
        
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var subscriptionEntity = await _subscriptionRepository.GetByIdAsync(id, cancellationToken);
            if (subscriptionEntity is null)
            {
                return;
            }
            await _subscriptionRepository.DeleteAsync(subscriptionEntity, cancellationToken);
            var subscriptionDeleted = subscriptionEntity.Adapt<SubscriptionDeleted>();
            await _eventBus.PublishAsync(subscriptionDeleted, cancellationToken);
        }

        public async Task<List<Subscription>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _subscriptionRepository.GetAllAsync(cancellationToken);
            return entities.Adapt<List<Subscription>>();
        }

        public async Task<Subscription> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _subscriptionRepository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Entity with this id doesn't exist"); 
            return entity.Adapt<Subscription>();
        }
        public async Task<Subscription> UpdateAsync(Guid id, SubscriptionType subscriptionType, CancellationToken cancellationToken)
        {
            var subscriptionToUpdate = await _subscriptionRepository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Entity with this id doesn't exist");
            subscriptionToUpdate.SubscriptionType = subscriptionType;
            await _subscriptionRepository.UpdateAsync(id, subscriptionToUpdate, cancellationToken);

            var updatedSubscription = await _subscriptionRepository.GetByIdAsync(id, cancellationToken);
            var subscriptionUpdated = updatedSubscription.Adapt<SubscriptionUpdated>();
            await _eventBus.PublishAsync(subscriptionUpdated, cancellationToken);
            return updatedSubscription.Adapt<Subscription>();
        }
    }   
}
