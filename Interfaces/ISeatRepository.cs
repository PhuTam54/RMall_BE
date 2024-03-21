using RMall_BE.Models;

namespace RMall_BE.Interfaces
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
