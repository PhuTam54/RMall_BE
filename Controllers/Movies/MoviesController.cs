using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Data;
using RMall_BE.Dto.MoviesDto;
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
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(400)]
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
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieByGenreId(int genreId)
        {
            if (!_genreRepository.GenreExist(genreId))
                return NotFound("Genre Not Found!");

            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovieByGenreId(genreId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movies);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovie([FromBody] MovieDto movieCreate)
        {
            if (movieCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieMap = _mapper.Map<Movie>(movieCreate);


            if (!_movieRepository.CreateMovie(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            foreach (int genreId in movieCreate.GenreIds)
            {
                Genre genre = _genreRepository.GetGenreById(genreId);
                if (genre != null)
                {
                    var movieGenre = new MovieGenre { Movie_Id = movieMap.Id, Genre_Id = genre.Id, Movie = movieMap, Genre = genre };
                    _movieRepository.CreateMovieGenre(movieGenre);
                }
                else
                {
                    return NotFound("Genre Not Found!");
                }
            }

            foreach (int languageId in movieCreate.LanguageIds)
            {
                Language language = _languageRepository.GetLanguageById(languageId);
                if (language != null)
                {
                    var languageMovie = new MovieLanguage { Movie_id = movieMap.Id, Language_id = language.Id, Movie = movieMap, Language = language };
                    _movieRepository.CreateMovieLanguage(languageMovie);
                }
                else
                {
                    return NotFound("Language Not Found!");
                }
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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

            return NoContent();
        }
    }
}
