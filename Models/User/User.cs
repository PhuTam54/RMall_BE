using Microsoft.VisualBasic;
using RMall_BE.Models.Movies;
using RMall_BE.Models.Orders;

namespace RMall_BE.Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
    }
}
