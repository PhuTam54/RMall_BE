namespace RMall_BE.Models.Movies.Genres
{
    public class MovieGenre
    {
        public int Id { get; set; }
        public int Movie_Id { get; set; }
        public int Genre_Id { get; set; }
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
