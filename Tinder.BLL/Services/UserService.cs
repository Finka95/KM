using AutoMapper;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class UserService : GenericService<User, UserEntity>, IUserService
    {

        public UserService(IUserRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
           
        }

        public async Task<User> UpdateUserByIdAsync(Guid id, User user, CancellationToken cancellationToken)
        {
            var newUser = _mapper.Map<UserEntity>(user);
            newUser.Id = id;
            var updatedUser = await _repository.UpdateAsync(newUser, cancellationToken);
            return _mapper.Map<User>(updatedUser);
        }
    }
}
