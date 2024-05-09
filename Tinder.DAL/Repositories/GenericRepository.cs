using Tinder.DAL.Interfaces;
using Tinder.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tinder.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
           await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
           await _context.SaveChangesAsync(cancellationToken);
           return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return entities;
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<TEntity>()
                .FindAsync(id, cancellationToken);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        public virtual async Task<TEntity> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
