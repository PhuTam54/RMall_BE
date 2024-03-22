using RMall_BE.Models.Movies.Genres;
using RMall_BE.Models.Movies.Languages;

namespace RMall_BE.Models.Movies
{
    public class Movie
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
        public ICollection<Show>? Shows { get; set; }
        public ICollection<MovieGenre>? MovieGenres { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public ICollection<GalleryMovie>? GalleryMovies { get; set; }
        public ICollection<MovieLanguage>? MovieLanguages { get; set; }

    }
}
