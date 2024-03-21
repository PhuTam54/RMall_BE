using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Data;
using RMall_BE.Dto;
using RMall_BE.Interfaces;
using RMall_BE.Models;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper)
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

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovie(int id, [FromBody] MovieDto updatedMovie)
        {
            if (!_movieRepository.MovieExist(id))
                return NotFound();
            if (updatedMovie == null)
                return BadRequest(ModelState);

            if (id != updatedMovie.id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var movieMap = _mapper.Map<Movie>(updatedMovie);
            if (!_movieRepository.UpdateMovie(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong updating reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
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

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_movieRepository.DeleteMovie(movieToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }

            return NoContent();
        }
    }
}
