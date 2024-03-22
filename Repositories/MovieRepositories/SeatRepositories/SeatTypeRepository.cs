using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Repositories.MovieRepositories.SeatRepositories
{
    public class SeatTypeRepository : ISeatTypeRepository
    {
        private readonly RMallContext _context;

        public SeatTypeRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateSeatType(SeatType seatType)
        {
            _context.Add(seatType);
            return Save(); ;
        }
        public bool UpdateSeatType(SeatType seatType)
        {
            _context.Update(seatType);
            return Save();
        }
        public bool DeleteSeatType(SeatType seatType)
        {
            _context.Remove(seatType);
            return Save();
        }

        public ICollection<SeatType> GetAllSeatType()
        {
            var seatTypes = _context.SeatTypes.ToList();
            return seatTypes;
        }

        public SeatType GetSeatTypeById(int id)
        {
            return _context.SeatTypes.FirstOrDefault(st => st.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SeatTypeExist(int id)
        {
            return _context.SeatTypes.Any(st => st.Id == id);
        }

        
    }
}
