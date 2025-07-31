using BookShop.Data.DTOs;
using BookShop.Data.Entities;

namespace BookShop.Data.Services
{
    public interface IOrderService : IService<Order, OrderDto>
    {
        int Add(OrderDto orderDto);
        IEnumerable<OrderDto> Filter(int id = 0, DateOnly? date = null);
    }
}
