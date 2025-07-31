namespace BookShop.Data.Services
{
    public interface IService<TEntity, TDto> where TEntity : class where TDto : class
    {
        TDto GetById(int id);
        IEnumerable<TDto> GetAll();
    }
}
