using RMall_BE.Data;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Models.Orders;

namespace RMall_BE.Repositories.OrderRepositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly RMallContext _context;

        public FoodRepository(RMallContext context)
        {
            _context = context;
        }
        public ICollection<Food> GetAllFood()
        {
            var foods = _context.Foods.ToList();
            return foods;
        }

        public Food GetFoodById(int id)
        {
            return _context.Foods.FirstOrDefault(c => c.Id == id);
        }

        public bool CreateFood(Food food)
        {
            _context.Add(food);
            return Save();
        }

        public bool UpdateFood(Food food)
        {
            _context.Update(food);
            return Save();
        }

        public bool DeleteFood(Food food)
        {
            _context.Remove(food);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool FoodExist(int id)
        {
            return _context.Foods.Any(c => c.Id == id);
        }

        
    }
}
