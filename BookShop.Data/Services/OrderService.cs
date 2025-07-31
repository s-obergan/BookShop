using BookShop.Data.DTOs;
using BookShop.Data.Entities;
using BookShop.Data.Repository;
using System;
namespace BookShop.Data.Services
{
    public class OrderService : Service<Order, OrderDto>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository repository) : base(repository)
        {
            _orderRepository = repository;
        }

        public int Add(OrderDto orderDto)
        {
            if (orderDto == null)
            {
                throw new ArgumentNullException(nameof(orderDto));
            }

            var order = ConvertToEntity(orderDto);
            return _orderRepository.Add(order);
        }

        public IEnumerable<OrderDto> Filter(int id = 0, DateOnly? date = null)
        {
            var orders = _orderRepository.Filter(id, date);
            return orders.Select(ConvertToDto);
        }

        protected override OrderDto ConvertToDto(Order entity)
        {
            return new OrderDto(entity);
        }

        protected override Order ConvertToEntity(OrderDto dto)
        {
            return dto.ConvertToEntity();
        }
    }
}
