using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Interfaces;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies.Genres;
using RMall_BE.Repositories.MovieRepositories;

namespace RMall_BE.Controllers.Movies
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GalleryMoviesController : Controller
    {
        private readonly IGalleryMovieRepository _galleryMovieRepository;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;

        public GalleryMoviesController(IGalleryMovieRepository galleryMovieRepository, IMapper mapper, IMovieRepository movieRepository)
        {
            _galleryMovieRepository = galleryMovieRepository;
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IActionResult GetAllGalleryMovie()
        {

            var galleryMovies = _mapper.Map<List<GalleryMovieDto>>(_galleryMovieRepository.GetAllGalleryMovie());

            return Ok(galleryMovies);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(GalleryMovie))]
        [ProducesResponseType(400)]
        public IActionResult GetGalleryMovieById(int id)
        {
            if (!_galleryMovieRepository.GalleryMovieExist(id))
                return NotFound();

            var galleryMovie = _mapper.Map<GalleryMovieDto>(_galleryMovieRepository.GetGalleryMovieById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(galleryMovie);
        }

        /// <summary>
        /// Create GalleryMovie
        /// </summary>
        /// <param name="GalleryMovieCreate"></param>
        /// <returns></returns>
        /// <remarks>
        /// POST/GalleryMovie
        /// {
        /// 
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGalleryMovie([FromQuery]int movieId,[FromBody] GalleryMovieDto galleryMovieCreate)
        {
            if (!_movieRepository.MovieExist(movieId))
                return NotFound("Movie Not Found!");
            if (galleryMovieCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var galleryMovieMap = _mapper.Map<GalleryMovie>(galleryMovieCreate);
            galleryMovieMap.Movie = _movieRepository.GetMovieById(movieId);

            if (!_galleryMovieRepository.CreateGalleryMovie(galleryMovieMap))
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
        public IActionResult UpdateGalleryMovie(int id, [FromBody] GalleryMovieDto updatedGalleryMovie)
        {
            if (!_galleryMovieRepository.GalleryMovieExist(id))
                return NotFound();
            if (updatedGalleryMovie == null)
                return BadRequest(ModelState);

            if (id != updatedGalleryMovie.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var galleryMovieMap = _mapper.Map<GalleryMovie>(updatedGalleryMovie);
            if (!_galleryMovieRepository.UpdateGalleryMovie(galleryMovieMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Gallery Movie!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGalleryMovie(int id)
        {
            if (!_galleryMovieRepository.GalleryMovieExist(id))
            {
                return NotFound();
            }

            var galleryMovieToDelete = _galleryMovieRepository.GetGalleryMovieById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_galleryMovieRepository.DeleteGalleryMovie(galleryMovieToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Gallery Movie!");
            }

            return NoContent();
        }
    }
}
