using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Dto.OrdersDto
{
    public class OrderDto
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
        public bool Is_Paid { get; set; }
        public string QR_Code { get; set; }
    }
}
