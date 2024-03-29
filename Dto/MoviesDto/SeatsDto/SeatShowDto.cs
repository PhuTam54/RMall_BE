using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Dto.MoviesDto.SeatsDto
{
    public class SeatShowDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
