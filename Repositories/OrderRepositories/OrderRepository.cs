using RMall_BE.Data;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Orders;

namespace RMall_BE.Repositories.OrderRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RMallContext _context;

        public OrderRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }
        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }
        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
        }

        public ICollection<Order> GetAllOrder()
        {
            var orders = _context.Orders.ToList();
            return orders;
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public bool OrderExist(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}
