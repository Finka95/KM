using AutoMapper;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using Shared.Events;
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
            var email = userJson["email"].ToString();

            var user = new User
            {
                FusionUserId = fusionUserId,
                FirstName = firstName,
                LastName = lastName,
                Age = birthDate.CalculateAge(),
                Email = email
            };

            var userEntity = _mapper.Map<UserEntity>(user);
            var createdUser = await _userRepository.CreateFromJsonAsync(userEntity, cancellationToken);

            var userCreatedEvent = _mapper.Map<UserCreated>(createdUser);
            await _eventBus.PublishAsync(userCreatedEvent, cancellationToken);
            return _mapper.Map<User>(createdUser);
        }
    }
}
