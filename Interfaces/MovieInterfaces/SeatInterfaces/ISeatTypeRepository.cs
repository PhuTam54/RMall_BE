using RMall_BE.Models;
using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces
{
    public interface ISeatTypeRepository
    {
        ICollection<SeatType> GetAllSeatType();
        SeatType GetSeatTypeById(int id);
        bool CreateSeatType(SeatType seatType);
        bool UpdateSeatType(SeatType seatType);
        bool DeleteSeatType(SeatType seatType);
        bool SeatTypeExist(int id);
        bool Save();
    }
}
