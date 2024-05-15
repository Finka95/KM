using AutoMapper;
using Tinder.BLL.Interfaces;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class GenericService<TModel, TEntity> : IGenericService<TModel> where TModel : class where TEntity : IBaseEntity
    {
        protected IGenericRepository<TEntity> _repository;
        protected IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TModel> CreateModelAsync(TModel model, CancellationToken cancellationToken)
        {
            var entity = await _repository.CreateAsync(_mapper.Map<TEntity>(model), cancellationToken);
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> DeleteModelAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.DeleteByIdAsync(id, cancellationToken);
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<TModel>>(entities);
        }

        public virtual async Task<TModel> GetModelByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<TModel>(entity);
        }

        public virtual async Task<TModel> UpdateModelAsync(TModel model, CancellationToken cancellationToken)
        {
            var entity = await _repository.UpdateAsync(_mapper.Map<TEntity>(model), cancellationToken);
            return _mapper.Map<TModel>(entity);
        }
    }
}
