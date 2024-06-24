using Microsoft.EntityFrameworkCore;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.DAL.Repositories
{
    public class LikeRepository : GenericRepository<LikeEntity>, ILikeRepository
    {
        public LikeRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public Task<List<LikeEntity>> GetAllUserSentLikesAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _context.Likes.Where(l => l.SenderId == userId).ToListAsync(cancellationToken);
        }
    }
}
