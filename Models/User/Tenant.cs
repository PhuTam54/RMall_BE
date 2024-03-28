using Microsoft.VisualBasic;
using RMall_BE.Models.Movies;
using RMall_BE.Models.Orders;

namespace RMall_BE.Models.User
{
    public class Tenant : User
    {
        public string FullName { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public string Phone_Number { get; set; }
        public string Address { get; set; }
        // Contracts...
        // public ICollection<Contract> Contracts { get; set; }

    }
}
