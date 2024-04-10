using Microsoft.VisualBasic;
using RMall_BE.Models.Movies;
using RMall_BE.Models.Orders;

namespace RMall_BE.Models.User
{
    public class Customer : User
    {
        public string FullName { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public string Phone_Number { get; set; }
        public string Address { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
