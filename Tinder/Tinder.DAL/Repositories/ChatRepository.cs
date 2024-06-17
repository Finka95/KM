using Microsoft.EntityFrameworkCore;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.DAL.Repositories
{
    public class ChatRepository : GenericRepository<ChatEntity>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public override Task<ChatEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _dbSet
                .AsNoTracking()
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public Task<ChatEntity> GetByIdWithUsersAndMessagesAsync(Guid id, CancellationToken cancellationToken)
        {
            return _dbSet
                .AsNoTracking()
                .Include(c => c.Users)
                .Include(c => c.Messages)
                .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
    }
}
