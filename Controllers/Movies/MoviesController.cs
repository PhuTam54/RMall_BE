using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Data;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models.Movies;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers.Movies
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
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
