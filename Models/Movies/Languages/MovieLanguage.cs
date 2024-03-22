namespace RMall_BE.Models.Movies.Languages
{
    public class MovieLanguage
    {
        public int Id { get; set; }
        public int Movie_id { get; set; }
        public int Language_id { get; set; }
        public Movie Movie { get; set; }
        public Language Language { get; set; }
    }
}
