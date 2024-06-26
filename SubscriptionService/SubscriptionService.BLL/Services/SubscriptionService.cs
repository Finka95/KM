﻿using SubscriptionService.BLL.Interfaces;
using SubscriptionService.DAL.Interfaces;
using SubscriptionService.BLL.Models;
using SubscriptionService.DAL.Entities;
using SubscriptionService.Domain.Enums;
using Mapster;
using Shared.Events;
using SubscriptionService.BLL.MessageBroker.Interfaces;
using SubscriptionService.Domain.Exceptions;
using SubscriptionService.Domain.Interfaces;

namespace SubscriptionService.BLL.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEventBus _eventBus;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository,
            IDateTimeProvider dateTimeProvider, IEventBus eventBus)
        {
            _subscriptionRepository = subscriptionRepository;
            _dateTimeProvider = dateTimeProvider;
            _eventBus = eventBus;
        }

        public async Task<Subscription> CreateAsync(Guid fusionUserId, 
            Subscription subscription, CancellationToken cancellationToken)
        {
            var userSubscription = await _subscriptionRepository.GetByFusionUserIdAsync(fusionUserId, cancellationToken);
            if (userSubscription != null)
            {
                throw new BadRequestException("User already has subscription");
            }

            var utcNow = _dateTimeProvider.UtcNow;
            var modelToCreate = new SubscriptionEntity
            {
                FusionUserId = fusionUserId,
                SubscriptionType = subscription.SubscriptionType,
                Email = subscription.Email,
                CreatedAt = utcNow,
                UpdatedAt = utcNow,
                ExpiresAt = utcNow.AddMonths(1)
            };
            var entity = await _subscriptionRepository.CreateAsync(modelToCreate, cancellationToken);

            var subscriptionCreated = entity.Adapt<SubscriptionCreated>();
            await _eventBus.PublishAsync(subscriptionCreated, cancellationToken);
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
        public async Task<Subscription> UpdateAsync(Guid id, SubscriptionType subscriptionType,
            CancellationToken cancellationToken)
        {
            var subscriptionToUpdate = await _subscriptionRepository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Entity with this id doesn't exist");
            var utcNow = _dateTimeProvider.UtcNow;
            subscriptionToUpdate.SubscriptionType = subscriptionType;
            subscriptionToUpdate.UpdatedAt = utcNow;
            subscriptionToUpdate.ExpiresAt = utcNow.AddMonths(1);
            await _subscriptionRepository.UpdateAsync(id, subscriptionToUpdate, cancellationToken);

            var subscriptionUpdated = subscriptionToUpdate.Adapt<SubscriptionUpdated>();
            await _eventBus.PublishAsync(subscriptionUpdated, cancellationToken);
            return subscriptionToUpdate.Adapt<Subscription>();
        }
    }
}