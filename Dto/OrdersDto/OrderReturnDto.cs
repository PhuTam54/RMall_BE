using RMall_BE.Dto.MoviesDto;
using RMall_BE.Models.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Dto.OrdersDto
{
    public class OrderReturnDto
    {
        public int Id { get; set; }
        public string Order_Code { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount_Amount { get; set; }
        public string Discount_Code { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Final_Total { get; set; }
        public int Status { get; set; }
        public string Payment_Method { get; set; }
        public bool Is_Paid { get; set; } = false;
        public string QR_Code { get; set; }
        public int User_Id { get; set; }
        public int Show_Id { get; set; }
        public ICollection<OrderFoodDto>? OrderFoods { get; set; }
        public ICollection<TicketDto>? Tickets { get; set; }
        public ShowDto Show { get; set; }
    }
}
