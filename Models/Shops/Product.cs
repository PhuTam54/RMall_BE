using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Models.Shops
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Shop_Id { get; set; }
        public Shop Shop { get; set; }
    }
}
