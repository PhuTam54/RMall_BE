using RMall_BE.Models;
using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces
{
    public interface ISeatShowRepository
    {
        ICollection<SeatShow> GetAllSeatShow();
        SeatShow GetSeatShowById(int id);
        ICollection<SeatShow> GetSeatShowByShowId(int showId);
        bool CreateSeatShow(SeatShow seatShow);
        bool UpdateSeatShow(SeatShow seatShow);
        bool DeleteSeatShow(SeatShow seatShow);
        bool SeatShowExist(int id);
        bool Save();
    }
}
