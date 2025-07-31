using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Data.Entities
{
    public class OrderLine
    {
        [Key]
        public int OrderLineId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Order Order { get; set; }
        public virtual Book OrderLineBook { get; set; }
        public int Quantity { get; set; } = 1;

        //can be extended with additional properties like OrderLinePrice TotalPrice in order, etc.
    }
}
