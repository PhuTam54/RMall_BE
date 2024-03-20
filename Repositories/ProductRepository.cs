using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly RMallContext _context;

        public ProductRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
        }
        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
        public bool DeleteProduct(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public ICollection<Product> GetAllProduct()
        {
            var products = _context.Products.ToList();
            return products;
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
        public Product GetProductByName(string name)
        {
            return _context.Products.FirstOrDefault(p => p.Name == name);
        }
        public bool ProductExist(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}
