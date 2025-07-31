using BookShop.Data.Entities;


namespace BookShop.Data.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        int Add(Order order);
        List<Order> Filter(int id = 0, DateOnly? date = null);
    }
}
