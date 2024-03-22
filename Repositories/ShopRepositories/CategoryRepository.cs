using RMall_BE.Data;
using RMall_BE.Interfaces.ShopInterfaces;
using RMall_BE.Models.Shops;

namespace RMall_BE.Repositories.ShopRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RMallContext _context;

        public CategoryRepository(RMallContext context)
        {
            _context = context;
        }
        public ICollection<Category> GetAllCategory()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public ICollection<Shop> GetShopByCategory(int categoryId)
        {
            return _context.Shops.Where(s => s.Category_Id == categoryId).ToList();
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool CategoryExist(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }


    }
}
