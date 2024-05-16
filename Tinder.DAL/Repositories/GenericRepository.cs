using Tinder.DAL.Interfaces;
using Tinder.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tinder.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected ApplicationDbContext _context;
        protected DbSet<TEntity> _dbSet; 

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
           _dbSet.Attach(entity);
           await _context.SaveChangesAsync(cancellationToken);
           return entity;
        }

        public virtual Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _dbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        } 

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbSet.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        public virtual async Task<TEntity> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
