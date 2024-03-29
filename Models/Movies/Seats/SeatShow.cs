using RMall_BE.Models.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Models.Movies.Seats
{
    public class SeatShow
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Show_Id { get; set; }
        public int SeatType_Id { get; set; }

        public Show Show { get; set; }
        public SeatType SeatType { get; set; }
    }
}
