namespace Tinder.BLL.Interfaces
{
    public interface IGenericService<TModel>
    {
        Task<TModel> CreateAsync(TModel model, CancellationToken cancellationToken);
        Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<TModel> UpdateAsync(Guid id, TModel model, CancellationToken cancellationToken);
        Task<TModel> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
