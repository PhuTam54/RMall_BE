﻿using RMall_BE.Models.Movies;
using RMall_BE.Models.Movies.Genres;
using RMall_BE.Models.Movies.Languages;

namespace RMall_BE.Interfaces.MovieInterfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetAllMovie();
        ICollection<Movie> GetAllMovieForAdmin();
        Movie GetMovieById(int id);
        ICollection<Movie> GetMovieByGenreId(int genreId);
        ICollection<Movie> GetMovieByLanguageId(int languageId);
        bool CreateMovieGenre(MovieGenre movieGenre);
        bool CreateMovieLanguage(MovieLanguage movieLanguage);
        bool DeleteMovieGenresByMovieId(int id);
        bool DeleteMovieLanguagesByMovieId(int id);
        bool CreateMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        bool DeleteMovie(Movie movie);
        bool MovieExist(int id);
        bool Save();
        double ConvertTimeToDouble(string time);
    }
}
