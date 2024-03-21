using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Repositories
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

        public Food GetFoodyById(int id)
        {
            return _context.Foods.FirstOrDefault(c => c.id == id);
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
            return _context.Foods.Any(c => c.id == id);
        }

        public Food GetFoodById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
