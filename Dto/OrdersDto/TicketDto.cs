using RMall_BE.Dto.MoviesDto.SeatsDto;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMall_BE.Dto.OrdersDto
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public bool Is_Used { get; set; }
        public SeatDto Seat { get; set; }
    }
}
