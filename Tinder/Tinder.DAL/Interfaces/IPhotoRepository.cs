using Tinder.DAL.Entities;

namespace Tinder.DAL.Interfaces
{
    public interface IPhotoRepository : IGenericRepository<PhotoEntity>
    {
        Task<PhotoEntity> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
        Task<PhotoEntity> DeleteAsync(PhotoEntity photo, CancellationToken cancellationToken);
        Task<List<PhotoEntity>> UpdateRangeAsync(List<PhotoEntity> entities, CancellationToken cancellationToken);
    }
}
