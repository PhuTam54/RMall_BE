using RMall_BE.Models.Movies.Seats;
using RMall_BE.Models.Orders;

namespace RMall_BE.Models.Movies
{
    public class Show
    {
        public int Id { get; set; }
        public string Show_Code { get; set; }
        public DateTime Start_Date { get; set; }
        public int Movie_Id { get; set; }
        public int Room_Id { get; set; }
        public Movie Movie { get; set; }
        public Room Room { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<SeatPricing> SeatPricings { get; set; }


    }
}
