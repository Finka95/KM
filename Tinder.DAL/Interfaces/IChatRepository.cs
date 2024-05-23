using Tinder.DAL.Entities;

namespace Tinder.DAL.Interfaces
{
    public interface IChatRepository : IGenericRepository<ChatEntity>
    {
        Task<ChatEntity> GetByIdWithUsersAndMessagesAsync(Guid id, CancellationToken cancellationToken);
    }
}
