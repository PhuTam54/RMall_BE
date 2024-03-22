using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces
{
    public interface ISeatRepository
    {
        ICollection<Seat> GetAllSeat();
        Seat GetSeatById(int id);
        bool CreateSeat(Seat seat);
        bool UpdateSeat(Seat seat);
        bool DeleteSeat(Seat seat);
        bool SeatExist(int id);
        bool Save();
    }
}
