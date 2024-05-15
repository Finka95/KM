namespace Tinder.BLL.Interfaces
{
    public interface IGenericService<TModel>
    {
        Task<TModel> CreateModelAsync(TModel model, CancellationToken cancellationToken);
        Task<TModel> GetModelByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<TModel> UpdateModelAsync(TModel model, CancellationToken cancellationToken);
        Task<TModel> DeleteModelAsync(Guid id, CancellationToken cancellationToken);
    }
}
