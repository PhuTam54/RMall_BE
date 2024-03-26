﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models.Movies.Genres;

namespace RMall_BE.Controllers.Movies
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenresController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenresController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllGenre()
        {

            var movies = _mapper.Map<List<GenreDto>>(_genreRepository.GetAllGenre());

            return Ok(movies);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public IActionResult GetGenreById(int id)
        {
            if (!_genreRepository.GenreExist(id))
                return NotFound();

            var genre = _mapper.Map<GenreDto>(_genreRepository.GetGenreById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(genre);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGenre([FromBody] GenreDto genreCreate)
        {
            if (genreCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(genreCreate);


            if (!_genreRepository.CreateGenre(genreMap))
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
        public IActionResult UpdateMovie(int id, [FromBody] MovieDto updatedGenre)
        {
            if (!_genreRepository.GenreExist(id))
                return NotFound();
            if (updatedGenre == null)
                return BadRequest(ModelState);

            if (id != updatedGenre.Id)
                return BadRequest(ModelState);


            var movieMap = _mapper.Map<Genre>(updatedGenre);
            if (!_genreRepository.UpdateGenre(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Genre!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGenre(int id)
        {
            if (!_genreRepository.GenreExist(id))
            {
                return NotFound();
            }

            var genreToDelete = _genreRepository.GetGenreById(id);


            if (!_genreRepository.DeleteGenre(genreToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Genre!");
            }

            return NoContent();
        }
    }
}