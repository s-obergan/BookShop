using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateOnly Date { get; set; } //int property for book year would be enough

        //TODO: add other properties like ISBN, Price, Description, etc. if necessary
    }
}
