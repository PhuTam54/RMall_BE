using RMall_BE.Dto;
using RMall_BE.Models.Shops;

namespace RMall_BE.Interfaces.ShopInterfaces
{
    public interface IShopRepository
    {
        ICollection<Shop> GetAllShop();
        Shop GetShopById(int id);
        Shop GetShopByName(string name);
        ICollection<Shop> GetShopOfCategory(int categoryId);
        bool CreateShop(Shop shop);
        bool UpdateShop(Shop shop);
        bool DeleteShop(Shop shop);
        bool ShopExist(int id);
        bool Save();
    }
}
