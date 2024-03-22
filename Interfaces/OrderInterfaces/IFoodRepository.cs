using RMall_BE.Models.Orders;

namespace RMall_BE.Interfaces.OrderInterfaces
{
    public interface IFoodRepository
    {
        ICollection<Food> GetAllFood();
        Food GetFoodById(int id);
        bool CreateFood(Food food);
        bool UpdateFood(Food food);
        bool DeleteFood(Food food);
        bool FoodExist(int id);
        bool Save();
    }
}
