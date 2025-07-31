using BookShop.Data.Entities;

namespace BookShop.Data.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        int Add(Book book);

        List<Book> Filter(string? title = null, DateOnly? date = null);
    }
}
