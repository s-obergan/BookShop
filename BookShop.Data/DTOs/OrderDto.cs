using BookShop.Data.Entities;

namespace BookShop.Data.DTOs
{
    public class OrderDto
    {
        public DateOnly OrderDate { get; set; }

        public List<BookDto> Books { get; set; }

        public OrderDto()
        {

        }

        public OrderDto(Order order)
        {
            if (order == null)
                return;

            OrderDate = order.Date;
            Books = order.OrderLines.Select(o => new BookDto(o.OrderLineBook)).ToList();
        }

        //could be separated into interface IDto in case of numerous DTO entities in the project
        public Order ConvertToEntity()
        {
            Order order = new Order();
            order.Date = OrderDate;
            if (Books.Any())
            {
                foreach (var book in Books)
                {
                    OrderLine orderLine = new OrderLine();
                    orderLine.BookId = book.Id;
                    order.OrderLines.Add(orderLine);
                }
            }
            return order;
        }
    }
}
