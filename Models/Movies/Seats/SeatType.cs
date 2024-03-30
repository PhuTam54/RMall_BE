namespace RMall_BE.Models.Movies.Seats
{
    public class SeatType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seat> Seats { get; set; }
        public ICollection<SeatPricing> SeatPricings { get; set; }
    }
}
