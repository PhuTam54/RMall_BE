using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Repositories.MovieRepositories.SeatRepositories
{
    public class SeatShowRepository : ISeatShowRepository
    {
        private readonly RMallContext _context;

        public SeatShowRepository(RMallContext context)
        {
            _context  = context;
        }
        public bool CreateSeatShow(SeatShow seatShow)
        {
            _context.Add(seatShow);
            return Save();
        }
        public bool UpdateSeatShow(SeatShow seatShow)
        {
            _context.Update(seatShow);
            return Save();
        }
        public bool DeleteSeatShow(SeatShow seatShow)
        {
            _context.Remove(seatShow);
            return Save();
        }

        public ICollection<SeatShow> GetAllSeatShow()
        {
            var seatShows = _context.SeatShows.ToList();
            return seatShows;
        }

        public SeatShow GetSeatShowById(int id)
        {
            return _context.SeatShows.FirstOrDefault(ss => ss.Id == id);
        }
        public ICollection<SeatShow> GetSeatShowByShowId(int showId)
        {
            return _context.SeatShows.Where(ss => ss.Show.Id == showId).ToList();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SeatShowExist(int id)
        {
            return _context.SeatShows.Any(ss => ss.Id == id);
        }

        
    }
}
