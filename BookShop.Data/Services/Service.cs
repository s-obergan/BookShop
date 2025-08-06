using BookShop.Data.Exceptions;
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
            if(id <= 0)
                throw new ServiceException("Invalid entity ID");

            try
            {
                var entity = _repository.GetById(id);

                if (entity == null)
                    throw new NotFoundException($"Entity with ID {id} was not found");

                return ConvertToDto(entity);
            }
            catch(RepositoryException ex)
            {
                throw new ServiceException("Error accessing data", ex);
            }
        }

        protected abstract TDto ConvertToDto(TEntity entity);
        protected abstract TEntity ConvertToEntity(TDto dto);
    }
}
