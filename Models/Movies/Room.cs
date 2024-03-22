using RMall_BE.Models.Movies.Seats;

namespace RMall_BE.Models.Movies
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public ICollection<Show> Shows { get; set; }
        public ICollection<Seat> Seats { get; set; }

    }
}
