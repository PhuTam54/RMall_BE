using RMall_BE.Models.Shops;

namespace RMall_BE.Interfaces.ShopInterfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAllProduct();
        Product GetProductById(int id);
        Product GetProductByName(string name);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool ProductExist(int id);
        bool Save();
    }
}
