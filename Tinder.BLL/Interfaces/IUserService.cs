using Tinder.BLL.Models;

namespace Tinder.BLL.Interfaces
{
    public interface IUserService : IGenericService<User>
    {
        public Task<User> UpdateUserByIdAsync(Guid id, User user, CancellationToken cancellationToken);
    }
}
