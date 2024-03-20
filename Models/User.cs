using Microsoft.VisualBasic;

namespace RMall_BE.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone_Number { get; set; }
        public string Address { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        
        public ICollection<Order> Orders { get; set; }
        public ICollection<Card> Cards { get; set; }
    }
}
