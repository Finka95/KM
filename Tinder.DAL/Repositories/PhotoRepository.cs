using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.DAL.Repositories
{
    public class PhotoRepository : GenericRepository<PhotoEntity>, IPhotoRepository
    {
        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
