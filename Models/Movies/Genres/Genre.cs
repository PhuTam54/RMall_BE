namespace RMall_BE.Models.Movies.Genres
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public ICollection<MovieGenre>? MovieGenres { get; set; }
    }
}
