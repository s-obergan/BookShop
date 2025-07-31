using BookShop.Data.Repository;

namespace BookShop.Data.Services
{
    public abstract class Service<TEntity, TDto> : IService<TEntity, TDto> where TEntity : class where TDto : class
    {
        protected readonly IRepository<TEntity> _repository;

        protected Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            var entities = _repository.GetAll().ToList();
            return entities.Select(ConvertToDto);
        }

        public virtual TDto GetById(int id)
        {
            var entity = _repository.GetById(id);
            return ConvertToDto(entity);
        }

        protected abstract TDto ConvertToDto(TEntity entity);
        protected abstract TEntity ConvertToEntity(TDto dto);
    }
}
