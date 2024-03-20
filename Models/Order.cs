namespace RMall_BE.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public int Status { get; set; }
        public DateTime Timestamp { get; set; }
        public int User_Id { get; set; }
        public User User { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
