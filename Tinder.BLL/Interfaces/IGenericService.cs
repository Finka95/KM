namespace Tinder.BLL.Interfaces
{
    public interface IGenericService<T>
    {
        Task<T> CreateModelAsync(T model, CancellationToken cancellationToken);
        Task<T> GetModelByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> UpdateModelAsync(Guid id, T model, CancellationToken cancellationToken);
        Task<T> DeleteModelAsync(Guid id, CancellationToken cancellationToken);
    }
}
