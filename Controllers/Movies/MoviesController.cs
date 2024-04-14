using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Data;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models.Movies;
using RMall_BE.Models.Movies.Genres;
using RMall_BE.Models.Movies.Languages;
using RMall_BE.Repositories;
using RMall_BE.Repositories.MovieRepositories;

namespace RMall_BE.Controllers.Movies
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly ILanguageRepository _languageRepository;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper, IGenreRepository genreRepository, ILanguageRepository languageRepository)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _languageRepository = languageRepository;
        }

        [HttpGet]
        public IActionResult GetAllMovie()
        {

            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetAllMovie());

            return Ok(movies);
        }

        [HttpGet]
        [Route("admin")]
        public IActionResult GetAllMovieForAdmin()
        {

            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetAllMovieForAdmin());

            return Ok(movies);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetMovieById(int id)
        {
            if (!_movieRepository.MovieExist(id))
                return NotFound();

            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovieById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movie);
        }

        [HttpGet]
        [Route("genreId")]
        public IActionResult GetMovieByGenreId(int genreId)
        {
            if (!_genreRepository.GenreExist(genreId))
                return NotFound("Genre Not Found!");

            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovieByGenreId(genreId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movies);
        }

        [HttpGet]
        [Route("languageId")]
        public IActionResult GetMovieByLanguageId(int languageId)
        {
            if (!_languageRepository.LanguageExist(languageId))
                return NotFound("Language Not Found!");

            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovieByLanguageId(languageId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movies);
        }

        /// <summary>
        /// Create Movie
        /// </summary>
        /// <param name="movieCreate"></param>
        /// <remarks>
        ///  {
        ///  "title": "Exhuma",
        ///  "actor": "Lee Do-hyun2,Kim Go-eun2,...",
        ///  "movie_Image": "Cinema/pixner.net/boleto/demo/assets/images/movie/exhuma.jpg",
        ///  "cover_Image": "/assets/images/movie/exhuma.jpg",
        ///  "description": "Exhuma is a refreshing film that introduces East Asian burial culture and Feng Shui. The actors all performed their roles excellently, bringing depth and authenticity to the film. I appreciated the frequent moments of humor, which added a light touch to the story. However, those unfamiliar with Feng Shui may find some aspects confusing.",
        ///  "duration": "2 hrs 50 mins",
        ///  "director": "Jang Jae-hyun",
        ///  "favorite_Count": 987,
        ///  "trailer": "https://www.youtube.com/",
        ///  "genreIds": [
        ///    1, 2
        /// ],
        /// "languageIds": [
        ///    1, 2
        ///  ]
        ///  }
        /// </remarks>
        /// <returns></returns>
        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateMovie([FromBody] MovieDto movieCreate)
        {
            if (movieCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieMap = _mapper.Map<Movie>(movieCreate);

            var genres = new List<Genre>();
            var languages = new List<Language>();

            foreach (var genreId in movieCreate.GenreIds)
            {
                var genre = _genreRepository.GetGenreById(genreId);
                if (genre == null)
                {
                    return NotFound("Genre Not Found!");
                }
                genres.Add(genre);
            }

            foreach (var languageId in movieCreate.LanguageIds)
            {
                var language = _languageRepository.GetLanguageById(languageId);
                if (language == null)
                {
                    return NotFound("Language Not Found!");
                }
                languages.Add(language);
            }
            if (!_movieRepository.CreateMovie(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            foreach (var item in genres)
            {
                var movieGenre = new MovieGenre { Movie_Id = movieMap.Id, Genre_Id = item.Id, Movie = movieMap, Genre = item };
                _movieRepository.CreateMovieGenre(movieGenre);
            }

            foreach (var item in languages)
            {
                var languageMovie = new MovieLanguage { Movie_id = movieMap.Id, Language_id = item.Id, Movie = movieMap, Language = item };
                _movieRepository.CreateMovieLanguage(languageMovie);
            }
            return Ok("Successfully created");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPut]
        [Route("id")]
        public IActionResult UpdateMovie(int id, [FromBody] MovieDto updatedMovie)
        {
            if (!_movieRepository.MovieExist(id))
                return NotFound();
            if (updatedMovie == null)
                return BadRequest(ModelState);

            if (id != updatedMovie.Id)
                return BadRequest(ModelState);


            var movieMap = _mapper.Map<Movie>(updatedMovie);
            if (!_movieRepository.UpdateMovie(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Movie!");
                return StatusCode(500, ModelState);
            }

            var genres = new List<Genre>();
            var languages = new List<Language>();

            foreach (var genreId in updatedMovie.GenreIds)
            {
                var genre = _genreRepository.GetGenreById(genreId);
                if (genre == null)
                {
                    return NotFound("Genre Not Found!");
                }
                genres.Add(genre);
            }

            foreach (var languageId in updatedMovie.LanguageIds)
            {
                var language = _languageRepository.GetLanguageById(languageId);
                if (language == null)
                {
                    return NotFound("Language Not Found!");
                }
                languages.Add(language);
            }

            // Remove existing movie genres and languages
            _movieRepository.DeleteMovieGenresByMovieId(movieMap.Id);
            _movieRepository.DeleteMovieLanguagesByMovieId(movieMap.Id);

            if (!_movieRepository.UpdateMovie(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Movie!");
                return StatusCode(500, ModelState);
            }

            foreach (var item in genres)
            {
                var movieGenre = new MovieGenre { Movie_Id = movieMap.Id, Genre_Id = item.Id, Movie = movieMap, Genre = item };
                _movieRepository.CreateMovieGenre(movieGenre);
            }

            foreach (var item in languages)
            {
                var languageMovie = new MovieLanguage { Movie_id = movieMap.Id, Language_id = item.Id, Movie = movieMap, Language = item };
                _movieRepository.CreateMovieLanguage(languageMovie);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteMovie(int id)
        {
            if (!_movieRepository.MovieExist(id))
            {
                return NotFound();
            }

            var movieToDelete = _movieRepository.GetMovieById(id);


            if (!_movieRepository.DeleteMovie(movieToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Movie!");
            }
            //nếu cần thì sẽ thêm phần xóa MovieGenre và MovieLanguage
            return NoContent();
        }
    }
}
