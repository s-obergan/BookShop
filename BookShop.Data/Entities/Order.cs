using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public DateOnly Date { get; set; }
    }
}
