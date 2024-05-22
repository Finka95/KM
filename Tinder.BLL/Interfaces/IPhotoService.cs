using Tinder.BLL.Models;

namespace Tinder.BLL.Interfaces
{
    public interface IPhotoService : IGenericService<Photo>
    {
        Task<Photo> CreateAsync(Guid userId, Photo model, CancellationToken cancellationToken);
        Task<Photo> GetByIdAsync(Guid userId, Guid id, CancellationToken cancellationToken);
        Task<Photo> DeleteAsync(Guid userId, Guid id, CancellationToken cancellationToken);
        Task<Photo> UpdateAvatarAsync(Guid userId, Guid id, CancellationToken cancellationToken);
    }
}
