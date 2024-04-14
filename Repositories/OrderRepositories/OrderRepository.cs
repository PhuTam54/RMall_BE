using Microsoft.EntityFrameworkCore;
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
            var orders = _context.Orders
                .ToList();
            return orders;
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(o => o.OrderFoods)
                    .ThenInclude(of => of.Food)
                .Include(o => o.Tickets)
                    .ThenInclude(t => t.Seat)
                        .ThenInclude(s => s.SeatType)
                            .ThenInclude(st => st.SeatPricings)
                .FirstOrDefault(o => o.Id == id);
        }

        public Order GetOrderByOrderCode(string orderCode)
        {
            var order = _context.Orders
                .Include(o => o.OrderFoods)
                    .ThenInclude(of => of.Food)
                .Include(o => o.Tickets)
                    .ThenInclude(t => t.Seat)
                        .ThenInclude(s => s.SeatType)
                            .ThenInclude(st => st.SeatPricings)
                .FirstOrDefault(o => o.Order_Code == orderCode);
            return order;
        }

        public ICollection<Order> GetOrderByUserId(int userId)
        {
            return _context.Orders
                .Include(o => o.OrderFoods)
                    .ThenInclude(of => of.Food)
                .Include(o => o.Tickets)
                    .ThenInclude(t => t.Seat)
                        .ThenInclude(s => s.SeatType)
                            .ThenInclude(st => st.SeatPricings)
                .Where(o => o.User_Id == userId).ToList();
        }

        public bool CreateOrderFood(OrderFood orderFood)
        {
            _context.OrderFoods.Add(orderFood);
            return Save();
        }

        public bool OrderExist(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }

        public bool OrderExist(string orderCode)
        {
            return _context.Orders.Any(o => o.Order_Code == orderCode);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}
