using BookShop.Data.DTOs;
using BookShop.Data.Entities;
using BookShop.Data.Exceptions;
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
            try
            {
                var orders = _orderRepository.Filter(id, date);
                
                //if orders were not found we can throw NotFoundException here and handle in API controller with corresponding HTTP code
                //whether we should make it depends on preferences and project guidelines/architerture 

                return orders.Select(ConvertToDto);
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
