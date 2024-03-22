using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Models.Orders
{
    public class OrderFood
    {
        public int Id { get; set; }
        public int Order_Id { get; set; }
        public int Food_Id { get; set; }
        public int Qty { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public Food Food { get; set; }
        public Order Order { get; set; }
    }
}
