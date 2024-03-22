using Microsoft.VisualBasic;

namespace RMall_BE.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int Anount { get; set; }
        public string Stripe_Token { get; set; }
        public DateTime Created_Date { get; set; }
        public int Card_Id { get; set; }
        public Card Card { get; set; }
    }
}
