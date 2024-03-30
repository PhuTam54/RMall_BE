namespace RMall_BE.Models.Movies.Seats
{
    public class SeatReservation
    {
        public int Id { get; set; }
        //public int Show_Id { get; set; }
        public DateTime Reservation_Expires_At { get; set; }
        //public Show Show { get; set; }
        public int Seat_Id { get; set; }
        public Seat Seat { get; set; }
    }
}
