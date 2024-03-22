using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Models.Movies.Seats
{
    public class SeatPricing
    {
        public int Id { get; set; }
        public int Seat_Type_Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Show_Id { get; set; }
        public SeatType SeatType { get; set; }
        public Show Show { get; set; }
    }
}
