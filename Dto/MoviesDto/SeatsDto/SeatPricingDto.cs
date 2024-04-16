using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Dto.MoviesDto.SeatsDto
{
    public class SeatPricingDto
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Seat_Type_Id { get; set; }
        public int Show_Id { get; set; }
    }
}
