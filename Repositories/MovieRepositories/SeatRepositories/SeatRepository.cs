using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Models.Movies;
using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Repositories.MovieRepositories.SeatRepositories
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
            var seats = _context.Seats
                .Include(s => s.SeatType)
                .ThenInclude(st => st.SeatPricings)
                .ToList();
            return seats;
        }


        public Seat GetSeatById(int id)
        {
            var seat = _context.Seats
                .Include(s => s.SeatType)
                .ThenInclude(st => st.SeatPricings)
                .FirstOrDefault(x => x.Id == id);
            return seat;
        }

        public ICollection<Seat> GetSeatByRoomId(int roomId)
        {
            var seats = _context.Seats
                .Include(s => s.SeatType)
                .ThenInclude(st => st.SeatPricings)
                .Where(s => s.Room.Id == roomId).ToList();
            return seats;
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
            return _context.Seats.Any(f => f.Id == id);
        }

    }
}
