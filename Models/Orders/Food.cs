using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Models.Orders
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ICollection<OrderFood> OrderFoods { get; set; }
    }
}
