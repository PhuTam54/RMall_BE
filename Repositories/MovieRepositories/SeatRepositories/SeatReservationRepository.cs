using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Repositories.MovieRepositories.SeatRepositories
{
    public class SeatReservationRepository : ISeatReservationRepository
    {
        private readonly RMallContext _context;

        public SeatReservationRepository(RMallContext context)
        {
            _context = context;
        }
        public bool CreateSeatReservation(SeatReservation seatReservation)
        {
            _context.Add(seatReservation);
            return Save();
        }
        public bool UpdateSeatReservation(SeatReservation seatReservation)
        {
            _context.Update(seatReservation);
            return Save();
        }
        public bool DeleteSeatReservation(SeatReservation seatReservation)
        {
            _context.Remove(seatReservation);
            return Save();
        }

        public ICollection<SeatReservation> GetAllSeatReservation()
        {
            var seatReservations = _context.SeatReservations.ToList();
            return seatReservations;
        }

        public SeatReservation GetSeatReservationById(int id)
        {
            return _context.SeatReservations.FirstOrDefault(sr => sr.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SeatReservationExist(int id)
        {
            return _context.SeatReservations.Any(sr => sr.Id == id);
        }

        
    }
}
