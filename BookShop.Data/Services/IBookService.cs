using BookShop.Data.DTOs;
using BookShop.Data.Entities;

namespace BookShop.Data.Services
{
    public interface IBookService : IService<Book, BookDto>
    {
        IEnumerable<BookDto> Filter(string? title = null, DateOnly? date = null);
        int Add(BookDto bookDto);
    }
}
