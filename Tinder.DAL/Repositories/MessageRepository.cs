using Microsoft.EntityFrameworkCore;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.DAL.Repositories
{
    public class MessageRepository : GenericRepository<MessageEntity>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public override Task<MessageEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _dbSet
                .AsNoTracking()
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }
    }
}
