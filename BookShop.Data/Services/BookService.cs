using BookShop.Data.DTOs;
using BookShop.Data.Entities;
using BookShop.Data.Exceptions;
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
            try
            {
                var books = _bookRepository.Filter(title, date);

                //if books were not found we can throw NotFoundException here and handle in API controller with corresponding HTTP code
                //whether we should make it depends on preferences and project guidelines/architerture 

                return books.Select(ConvertToDto);
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException("Error searching books", ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Some error message", ex);
            }
        }

        public int Add(BookDto bookDto)
        {
            if (bookDto == null)
            {
                throw new ArgumentNullException(nameof(bookDto));
            }
            try
            {
                var book = ConvertToEntity(bookDto);
                return _bookRepository.Add(book);
            }
            catch(RepositoryException ex)
            {
                throw new ServiceException("Error adding new book", ex);
            }
            catch(Exception ex)
            {
                throw new ServiceException("Some error description", ex);
            }
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
