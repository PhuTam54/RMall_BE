using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Repositories.MovieRepositories.SeatRepositories
{
    public class SeatPricingRepository : ISeatPricingRepository
    {
        private readonly RMallContext _context;

        public SeatPricingRepository(RMallContext context)
        {
            _context  = context;
        }
        public bool CreateSeatPricing(SeatPricing SeatPricing)
        {
            _context.Add(SeatPricing);
            return Save();
        }
        public bool UpdateSeatPricing(SeatPricing SeatPricing)
        {
            _context.Update(SeatPricing);
            return Save();
        }
        public bool DeleteSeatPricing(SeatPricing SeatPricing)
        {
            _context.Remove(SeatPricing);
            return Save();
        }

        public ICollection<SeatPricing> GetAllSeatPricing()
        {
            var seatPricings = _context.SeatPricings.ToList();
            return seatPricings;
        }

        public SeatPricing GetSeatPricingById(int id)
        {
            return _context.SeatPricings.FirstOrDefault(ss => ss.Id == id);
        }
        public ICollection<SeatPricing> GetSeatPricingByShowId(int showId)
        {
            return _context.SeatPricings.Where(ss => ss.Show.Id == showId).ToList();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SeatPricingExist(int id)
        {
            return _context.SeatPricings.Any(ss => ss.Id == id);
        }

        
    }
}
