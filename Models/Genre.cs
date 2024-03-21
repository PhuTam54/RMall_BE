namespace RMall_BE.Models
{
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }

        public ICollection<GenreMovie> GenreMovies { get; set; }
    }
}
