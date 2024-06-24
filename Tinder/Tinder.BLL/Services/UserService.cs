using AutoMapper;
using Newtonsoft.Json.Linq;
using Shared.Events;
using System.Text.Json.Nodes;
using Tinder.BLL.Exceptions;
using Tinder.BLL.Extensions;
using Tinder.BLL.Interfaces;
using Tinder.BLL.MessageBroker.Interfaces;
using Tinder.BLL.Models;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class UserService : GenericService<User, UserEntity>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;

        public UserService(IUserRepository repository, IMapper mapper, IEventBus eventBus)
            : base(repository, mapper)
        {
            _userRepository = repository;
            _eventBus = eventBus;
        }

        public async Task<User> CreateUserFromJson(JsonObject request, CancellationToken cancellationToken)
        {
            var jsonObject = JObject.Parse(request.ToJsonString());
            var userJson = jsonObject["event"]["user"];

            var fusionUserId = Guid.Parse(userJson["id"].ToString());
            var firstName = userJson["firstName"].ToString();
            var lastName = userJson["lastName"].ToString();
            var birthDate = (DateTime)userJson["birthDate"];

            var user = new User
            {
                FusionUserId = fusionUserId,
                FirstName = firstName,
                LastName = lastName,
                Age = birthDate.CalculateAge()
            };

            var userEntity = _mapper.Map<UserEntity>(user);
            var createdUser = await _userRepository.CreateFromJsonAsync(userEntity, cancellationToken);
            var userCreatedEvent = _mapper.Map<UserCreated>(createdUser);
            await _eventBus.PublishAsync(userCreatedEvent, cancellationToken);
            return _mapper.Map<User>(createdUser);
        }

        public async Task<User> SetSubscriptionIdAsync(Guid fusionUserId, Guid subscriptionId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByFusionUserId(fusionUserId, cancellationToken);
            user.SubscriptionId = subscriptionId;
            await _userRepository.UpdateAsync(user, cancellationToken);
            return _mapper.Map<User>(user);
        }

        public override async Task<User> UpdateAsync(Guid id, User model, CancellationToken cancellationToken)
        {
            var userFromDb = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Entity with this id doesn't exist"); ;
            var newEntity = _mapper.Map<UserEntity>(model);
            newEntity.Id = id;
            newEntity.FusionUserId = userFromDb.FusionUserId;
            newEntity.SubscriptionId = userFromDb.SubscriptionId;
            await _repository.UpdateAsync(newEntity, cancellationToken);
            return _mapper.Map<User>(newEntity);
        }
    }
}
