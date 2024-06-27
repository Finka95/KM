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

        public async Task<UserEntity> CreateFromJsonAsync(UserEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
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

        public Task<UserEntity> GetByFusionUserId(Guid fusionUserId, CancellationToken cancellationToken)
        {
            return _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.FusionUserId == fusionUserId, cancellationToken);
        }

    }
}
