using RMall_BE.Dto;
using RMall_BE.Models;

namespace RMall_BE.Interfaces
{
    public interface IShopRepository
    {
        ICollection<Shop> GetAllShop();
        Shop GetShopById(int id);
        ICollection<Shop> GetShopOfCategory(int categoryId);
        bool CreateShop(Shop shop);
        bool UpdateShop(Shop shop);
        bool DeleteShop(Shop shop);
        bool ShopExist(int id);
        bool Save();
    }
}
