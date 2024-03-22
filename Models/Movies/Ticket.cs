using RMall_BE.Models.Movies.Seats;
using RMall_BE.Models.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Models.Movies
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime Start_Date { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public bool Is_Used { get; set; }
        public int Order_Id { get; set; }
        //public int Seat_Id { get; set; }
        public Order Order { get; set; }
        //public Seat Seat { get; set; }
    }
}
