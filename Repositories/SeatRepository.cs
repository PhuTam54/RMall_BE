using AutoMapper;
using RMall_BE.Data;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly RMallContext _context;
        private readonly IMapper _mapper;


        public SeatRepository(RMallContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public ICollection<Seat> GetAllSeat()
        {
            var seats = _context.Seats.ToList();
            return seats;
        }


        public Seat GetSeatById(int id)
        {
            return _context.Seats.FirstOrDefault(x => x.id == id);
        }
        public bool CreateSeat(Seat seat)
        {
            _context.Add(seat);
            return Save();
        }

        public bool DeleteSeat(Seat seat)
        {
            _context.Remove(seat);
            return Save();
        }


        public bool UpdateSeat(Seat seat)
        {
            _context.Update(seat);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool SeatExist(int id)
        {
            return _context.Seats.Any(f => f.id == id);
        }

      
    }
}
