using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly RMallContext _context;

        public ShopRepository(RMallContext context)
        {
            _context = context;
        }
        public ICollection<Shop> GetAllShop()
        {
            var shops = _context.Shops.ToList();
            return shops;
        }

        public Shop GetShopById(int id)
        {
            return _context.Shops.FirstOrDefault(s => s.Id == id);

        }
        public ICollection<Shop> GetShopOfCategory(int categoryId)
        {
            return _context.Shops.Where(s => s.Category_Id == categoryId).ToList();
        }
        public bool CreateShop(Shop shop)
        {
            _context.Add(shop);
            return Save();
        }
        public bool UpdateShop(Shop shop)
        {
            _context.Update(shop);
            return Save();
        }
        public bool DeleteShop(Shop shop)
        {
            _context.Remove(shop);
            return Save();
        }

        

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ShopExist(int id)
        {
            return _context.Shops.Any(s => s.Id == id);
        }

        
    }
}
