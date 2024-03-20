using RMall_BE.Helpers;

namespace RMall_BE.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public ICollection<Shop>? Shops { get; set; }
    }
}
