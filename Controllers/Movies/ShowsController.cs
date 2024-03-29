using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies;
using RMall_BE.Repositories.MovieRepositories;

namespace RMall_BE.Controllers.Movies
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : Controller
    {
        private readonly IShowRepository _showRepository;
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IFoodRepository _foodRepository;

        public ShowsController(IShowRepository showRepository, IMapper mapper, IMovieRepository movieRepository, IRoomRepository roomRepository, IFoodRepository foodRepository)
        {
            _showRepository = showRepository;
            _mapper = mapper;
            _movieRepository = movieRepository;
            _roomRepository = roomRepository;
            _foodRepository = foodRepository;
        }

        [HttpGet]
        public IActionResult GetAllShow()
        {

            var shows = _showRepository.GetAllShow();

            return Ok(shows);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(Show))]
        [ProducesResponseType(400)]
        public IActionResult GetShowById(int id)
        {
            if (!_showRepository.ShowExist(id))
                return NotFound();

            var show = _mapper.Map<ShowDto>(_showRepository.GetShowById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(show);
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateShow([FromQuery]int movieId, [FromQuery]int roomId,[FromBody] ShowDto showCreate)
        {
            if(!_movieRepository.MovieExist(movieId))
                return NotFound("Movie Not Found");
            if(!_roomRepository.RoomExist(roomId))
                return NotFound("Room Not Found");

            if (showCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var showMap = _mapper.Map<Show>(showCreate);
            showMap.Movie = _movieRepository.GetMovieById(movieId);
            showMap.Room = _roomRepository.GetRoomById(roomId);

            if (!_showRepository.CreateShow(showMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPut]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateShow(int id, [FromBody] ShowDto updatedShow)
        {
            if (!_showRepository.ShowExist(id))
                return NotFound();
            if (updatedShow == null)
                return BadRequest(ModelState);

            if (id != updatedShow.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var showMap = _mapper.Map<Show>(updatedShow);
            if (!_showRepository.UpdateShow(showMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Show!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteShow(int id)
        {
            if (!_showRepository.ShowExist(id))
            {
                return NotFound();
            }

            var showToDelete = _showRepository.GetShowById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_showRepository.DeleteShow(showToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Show!");
            }

            return NoContent();
        }
    }
}
