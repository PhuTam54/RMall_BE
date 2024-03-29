using RMall_BE.Models;
using RMall_BE.Models.Orders;

namespace RMall_BE.Interfaces.OrderInterfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetAllOrder();
        Order GetOrderById(int id);
        ICollection<Order> GetOrderByUserId(int userId);
        bool CreateOrderFood(OrderFood orderFood);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool OrderExist(int id);
        bool Save();
    }
}
