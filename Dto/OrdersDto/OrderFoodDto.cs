using RMall_BE.Models.Orders;

namespace RMall_BE.Dto.OrdersDto
{
    public class OrderFoodDto
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public int Food_Id { get; set; }
    }
}
