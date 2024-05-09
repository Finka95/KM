namespace Tinder.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : IBaseEntity
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
