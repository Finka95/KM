using Microsoft.EntityFrameworkCore;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.DAL.Repositories
{
    public class PhotoRepository : GenericRepository<PhotoEntity>, IPhotoRepository
    {

        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public override async Task<PhotoEntity> CreateAsync(PhotoEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public Task<PhotoEntity?> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
        {
            return _dbSet.AsNoTracking()
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId, cancellationToken);
        }

        public override Task<List<PhotoEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _dbSet.AsNoTracking().Include(p => p.User).ToListAsync(cancellationToken);
        }

       /* public async Task<List<PhotoEntity>> UpdateRangeAsync(List<PhotoEntity> photos, CancellationToken cancellationToken)
        {
            var entityIds = photos.Select(e => e.Id).ToList();
            var entitiesFromDb = await _dbSet.Where(e => entityIds.Contains(e.Id)).ToListAsync(cancellationToken);

            foreach (var entity in photos)
            {
                var entityFromDb = entitiesFromDb.Find(e => e.Id == entity.Id);
                if (entityFromDb != null)
                {
                    entityFromDb.PhotoURL = entity.PhotoURL;
                    entityFromDb.IsAvatar = entity.IsAvatar;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return photos;
        }*/

        public async Task<PhotoEntity> DeleteAsync(PhotoEntity photo, CancellationToken cancellationToken)
        {
            _dbSet.Remove(photo);
            await _context.SaveChangesAsync(cancellationToken);
            return photo;
        }
    }
}
