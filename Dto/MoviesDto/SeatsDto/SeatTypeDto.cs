using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Dto.MoviesDto.SeatsDto
{
    public class SeatTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SeatPricingDto> SeatPricings { get; set; }

    }
}
