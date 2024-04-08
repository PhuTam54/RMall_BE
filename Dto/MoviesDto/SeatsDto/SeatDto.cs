using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Dto.MoviesDto.SeatsDto
{
    public class SeatDto
    {
        public int Id { get; set; }
        public int Row_Number { get; set; }
        public int Seat_Number { get; set; }
        public SeatTypeDto SeatType { get; set; }
    }
}
