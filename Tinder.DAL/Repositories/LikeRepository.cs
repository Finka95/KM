using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.DAL.Repositories
{
    public class LikeRepository : GenericRepository<LikeEntity>, ILikeRepository
    {
        public LikeRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
