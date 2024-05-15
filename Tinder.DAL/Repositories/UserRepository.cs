using Microsoft.EntityFrameworkCore;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.DAL.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override Task<List<UserEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _dbSet.AsNoTracking()
                .Include(u => u.Photos)
                .Include(u => u.ReceivedLikes)
                .Include(u => u.SentLikes)
                .Include(u => u.Chats)
                .Include(u => u.Photos)
                .ToListAsync(cancellationToken);
        }

        public override Task<UserEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _dbSet.AsNoTracking()
                .Include(u => u.Photos)
                .Include(u => u.ReceivedLikes)
                .Include(u => u.SentLikes)
                .Include(u => u.Chats)
                .Include(u => u.Photos)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
