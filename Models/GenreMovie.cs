namespace RMall_BE.Models
{
    public class GenreMovie
    {
        public int id { get; set; }
        public int movie_id { get; set; }
        public int genre_id { get; set; }

        public Genre Genre { get; set; }
        public ICollection<Movie> Movies { get; set;}

    }
}
