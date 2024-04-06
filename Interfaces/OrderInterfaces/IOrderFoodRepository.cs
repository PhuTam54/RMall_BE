using RMall_BE.Models.Orders;

namespace RMall_BE.Interfaces.OrderInterfaces
{
    public interface IOrderFoodRepository
    {
        ICollection<OrderFood> GetAllOrderFood();
        OrderFood GetOrderFoodById(int id);
        bool CreateOrderFood(OrderFood orderfood);
        bool UpdateOrderFood(OrderFood orderfood);
        bool DeleteOrderFood(OrderFood orderfood);
        bool OrderFoodExist(int id);
        bool Save();
    }
}
