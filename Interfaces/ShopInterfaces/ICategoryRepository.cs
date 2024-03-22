using RMall_BE.Models.Shops;

namespace RMall_BE.Interfaces.ShopInterfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategory();
        Category GetCategoryById(int id);
        ICollection<Shop> GetShopByCategory(int categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool CategoryExist(int id);
        bool Save();
    }
}
