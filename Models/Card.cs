using RMall_BE.Models.User;

namespace RMall_BE.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Stripe_Token { get; set; }
        public DateTime Expired_Date { get; set; }
        public int Last_Four_Number { get; set; }
        public DateTime Created_Date { get; set; }
        public int User_Id { get; set; }
        public Customer User { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}
