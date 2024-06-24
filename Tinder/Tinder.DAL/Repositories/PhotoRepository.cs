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
        public async Task<PhotoEntity> DeleteAsync(PhotoEntity photo, CancellationToken cancellationToken)
        {
            _dbSet.Remove(photo);
            await _context.SaveChangesAsync(cancellationToken);
            return photo;
        }

        public async Task<List<PhotoEntity>> UpdateRangeAsync(List<PhotoEntity> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }
    }
}
