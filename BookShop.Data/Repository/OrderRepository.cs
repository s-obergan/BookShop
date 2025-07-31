using BookShop.Data.Entities;

namespace BookShop.Data.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ShopDbContext context) : base(context) { }

        public int Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.OrderId;
        }

        public List<Order> Filter(int id = 0, DateOnly? date = null)
        {
            //filtering may need pagination parameters depending on the number of items in database
            IQueryable<Order> query = _context.Orders.AsQueryable();

            if (id > 0)
            {
                query = query.Where(o => o.OrderId == id);
            }

            if (date.HasValue)
            {
                query = query.Where(o => o.Date == date.Value);
            }

            return query.ToList();
        }
    }
}
