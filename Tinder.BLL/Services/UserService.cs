using AutoMapper;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using Tinder.BLL.Extensions;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class UserService : GenericService<User, UserEntity>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _userRepository = repository;
        }

        public async Task<User> CreateUserFromJson(JsonObject request, CancellationToken cancellationToken)
        {
            var jsonObject = JObject.Parse(request.ToJsonString());
            var userJson = jsonObject["event"]["user"];

            var fusionId = Guid.Parse(userJson["id"].ToString());
            var firstName = userJson["firstName"].ToString();
            var lastName = userJson["lastName"].ToString();
            var birthDate = (DateTime)userJson["birthDate"];

            var user = new User
            {
                FusionId = fusionId,
                FirstName = firstName,
                LastName = lastName,
                Age = birthDate.Age()
            };

            var userEntity = _mapper.Map<UserEntity>(user);
            var createdUser = await _userRepository.CreateFromJsonAsync(userEntity, cancellationToken);
            return _mapper.Map<User>(createdUser);
        }
    }
}
