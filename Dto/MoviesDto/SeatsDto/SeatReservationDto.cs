namespace RMall_BE.Dto.MoviesDto.SeatsDto
{
    public class SeatReservationDto
    {
        public int Id { get; set; }
        public int Show_Id { get; set; }
        public int Seat_Id { get; set; }
        public DateTime Reservation_Expires_At { get; set; }
    }
}
