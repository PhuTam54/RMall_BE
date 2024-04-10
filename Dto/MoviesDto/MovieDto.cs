using RMall_BE.Dto.MoviesDto.SeatsDto;
using RMall_BE.Models.Movies.Languages;
using RMall_BE.Models.Movies;
using RMall_BE.Models.Movies.Genres;

namespace RMall_BE.Dto.MoviesDto
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Actor { get; set; }
        public string Movie_Image { get; set; }
        public string Cover_Image { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Director { get; set; }
        public int Favorite_Count { get; set; }
        public string Trailer { get; set; }
        public List<int> GenreIds { get; set; }
        public List<int> LanguageIds { get; set; }
        public ICollection<MovieGenreDto>? MovieGenres { get; set; }
        public ICollection<MovieLanguageDto>? MovieLanguages { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<GalleryMovieDto>? GalleryMovies { get; set; }
    }
}
