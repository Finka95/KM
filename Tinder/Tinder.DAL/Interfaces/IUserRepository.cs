using Tinder.DAL.Entities;

namespace Tinder.DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity> CreateFromJsonAsync(UserEntity entity, CancellationToken cancellationToken);
        Task<UserEntity> GetByFusionUserId(Guid fusionUserIdm, CancellationToken cancellationToken);
    }
}
