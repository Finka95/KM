using Tinder.BLL.Models;

namespace Tinder.BLL.Interfaces
{
    public interface IChatService : IGenericService<Chat>
    {
        Task<Chat> GetByIdWithUsersAndMessagesAsync(Guid id, CancellationToken cancellationToken);
    }
}
