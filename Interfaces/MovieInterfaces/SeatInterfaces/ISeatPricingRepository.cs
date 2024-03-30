using RMall_BE.Models;
using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces
{
    public interface ISeatPricingRepository
    {
        ICollection<SeatPricing> GetAllSeatPricing();
        SeatPricing GetSeatPricingById(int id);
        ICollection<SeatPricing> GetSeatPricingByShowId(int showId);
        bool CreateSeatPricing(SeatPricing seatPricing);
        bool UpdateSeatPricing(SeatPricing seatPricing);
        bool DeleteSeatPricing(SeatPricing seatPricing);
        bool SeatPricingExist(int id);
        bool Save();
    }
}
