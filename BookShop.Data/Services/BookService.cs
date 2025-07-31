using BookShop.Data.DTOs;
using BookShop.Data.Entities;
using BookShop.Data.Repository;


namespace BookShop.Data.Services
{
    public class BookService : Service<Book, BookDto>, IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository repository) : base(repository)
        {
            _bookRepository = repository;
        }

        public IEnumerable<BookDto> Filter(string? title = null, DateOnly? date = null)
        {
            var books = _bookRepository.Filter(title, date);
            return books.Select(ConvertToDto);
        }

        public int Add(BookDto bookDto)
        {
            if (bookDto == null)
            {
                throw new ArgumentNullException(nameof(bookDto));
            }

            var book = ConvertToEntity(bookDto);
            return _bookRepository.Add(book);
        }

        protected override BookDto ConvertToDto(Book entity)
        {
            return new BookDto(entity);
        }

        protected override Book ConvertToEntity(BookDto dto)
        {
            return dto.ConvertToEntity();
        }
    }
}
