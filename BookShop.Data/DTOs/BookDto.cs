using BookShop.Data.Entities;

namespace BookShop.Data.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateOnly Date { get; set; }

        //we could transform data entities to DTO using different approaches - in DTO constructor, using extension methods, mapper, etc. 
        //taking into account minimal amount of entities manual mapping looks reasonable

        public BookDto()
        {

        }
        public BookDto(Book entity)
        {
            if (entity == null)
                return;

            Id = entity.BookId;
            Author = entity.Author;
            Title = entity.Title;
            Date = entity.Date;
        }
        public Book ConvertToEntity()
        {
            Book book = new Book();
            book.Author = this.Author;
            book.Title = this.Title;
            book.Date = this.Date;

            return book;
        }
    }
}
