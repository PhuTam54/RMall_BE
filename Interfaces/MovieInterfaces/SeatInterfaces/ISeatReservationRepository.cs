using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces
{
    public interface ISeatReservationRepository
    {
        ICollection<SeatReservation> GetAllSeatReservation();
        SeatReservation GetSeatReservationById(int id);
        bool CreateSeatReservation(SeatReservation seatReservation);
        bool UpdateSeatReservation(SeatReservation seatReservation);
        bool DeleteSeatReservation(SeatReservation seatReservation);
        bool SeatReservationExist(int id);
        bool Save();
    }
}
