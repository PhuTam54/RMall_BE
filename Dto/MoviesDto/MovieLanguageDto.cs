using RMall_BE.Models.Movies.Languages;
using RMall_BE.Models.Movies;

namespace RMall_BE.Dto.MoviesDto
{
    public class MovieLanguageDto
    {
        public int Id { get; set; }
        public int Movie_id { get; set; }
        public int Language_id { get; set; }
        public LanguageDto Language { get; set; }
    }
}
