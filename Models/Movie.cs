namespace RMall_BE.Models
{
    public class Movie
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string duration { get; set; }
        public string director { get; set; }
        public string actors { get; set; }
        public string gerne { get; set; }

        public GenreMovie GenreMovies { get; set; }
        public ICollection<GenreMovie> genreMovies { get; set; }

    }
}
