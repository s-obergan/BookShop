using BookShop.Data.Exceptions;
using Microsoft.Data.SqlClient;
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

        public T GetById(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch(SqlException ex)
            {
                throw new RepositoryException("Database error occurred while fetching product", ex);
            }
            catch(Exception ex)
            {
                throw new RepositoryException("Error occurred in Repository", ex);
            }
        }

    }
}
