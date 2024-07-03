using GraphQlService.BLL.Models;

namespace GraphQlService.BLL.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
    }
}
