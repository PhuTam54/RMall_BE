using RMall_BE.Models;

namespace RMall_BE.Interfaces
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
