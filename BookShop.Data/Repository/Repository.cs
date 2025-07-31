using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ShopDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ShopDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        //Repository could be based on async methods if necessary
        public IQueryable<T> GetAll() => _dbSet;

        public T GetById(int id) => _dbSet.Find(id);

    }
}
