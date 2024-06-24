using Tinder.DAL.Entities;

namespace Tinder.DAL.Interfaces
{
    public interface ILikeRepository : IGenericRepository<LikeEntity>
    {
        public Task<List<LikeEntity>> GetAllUserSentLikesAsync(Guid userId, CancellationToken cancellationToken);
    }
}
