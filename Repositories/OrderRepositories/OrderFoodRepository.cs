using RMall_BE.Data;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Models.Orders;

namespace RMall_BE.Repositories.OrderRepositories
{
    public class OrderFoodRepository : IOrderFoodRepository
    {
        private readonly RMallContext _context;

        public OrderFoodRepository(RMallContext context)
        {
            _context = context;
        }

        public bool CreateOrderFood(OrderFood orderFood)
        {
            _context.Add(orderFood);
            return Save();
        }
        public bool UpdateOrderFood(OrderFood orderFood)
        {
            _context.Update(orderFood);
            return Save();
        }
        public bool DeleteOrderFood(OrderFood orderFood)
        {
            _context.Remove(orderFood);
            return Save();
        }

        public ICollection<OrderFood> GetAllOrderFood()
        {
            var orderFoods = _context.OrderFoods.ToList();
            return orderFoods;
        }

        public OrderFood GetOrderFoodById(int id)
        {
            return _context.OrderFoods.FirstOrDefault(t => t.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool OrderFoodExist(int id)
        {
            return _context.OrderFoods.Any(t => t.Id == id);
        }
    }
}
