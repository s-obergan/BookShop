using BookShop.Data.Entities;
using BookShop.Data.Exceptions;

namespace BookShop.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ShopDbContext context) : base(context) { }

        public int Add(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();

                return book.BookId;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding a new book", ex);
            }
        }

        public List<Book> Filter(string? title = null, DateOnly? date = null)
        {
            //filtering may need pagination parameters depending on the number of items in database
            IQueryable<Book> query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                title = title.Trim().ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(title)); //exact search condition (contains or equals or whatever else) depends on requirements, this is just an example
                //using ToLower() may be not necessary depending on search requirements and performance concerns
            }

            if (date.HasValue)
            {
                query = query.Where(b => b.Date == date.Value);
            }

            return query.ToList();
        }
    }
}
