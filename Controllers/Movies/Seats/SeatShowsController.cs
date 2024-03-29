using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Dto.MoviesDto.SeatsDto;
using RMall_BE.Interfaces;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies.Seats;
using RMall_BE.Repositories.MovieRepositories.SeatRepositories;

namespace RMall_BE.Controllers.Movies.Seats
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatShowsController : ControllerBase
    {
        private readonly ISeatShowRepository _seatShowRepository;
        private readonly IMapper _mapper;
        private readonly IShowRepository _showRepository;
        private readonly ISeatTypeRepository _seatTypeRepository;

        public SeatShowsController(ISeatShowRepository seatShowRepository, IMapper mapper, IShowRepository showRepository, ISeatTypeRepository seatTypeRepository)
        {
            _seatShowRepository = seatShowRepository;
            _mapper = mapper;
            _showRepository = showRepository;
            _seatTypeRepository = seatTypeRepository;
        }

        [HttpGet]
        public IActionResult GetAllSeatShow()
        {

            var seatShows = _mapper.Map<List<SeatShowDto>>(_seatShowRepository.GetAllSeatShow());

            return Ok(seatShows);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(SeatShow))]
        [ProducesResponseType(400)]
        public IActionResult GetSeatShowById(int id)
        {
            if (!_seatShowRepository.SeatShowExist(id))
                return NotFound();

            var seatShow = _mapper.Map<SeatShowDto>(_seatShowRepository.GetSeatShowById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(seatShow);
        }

        [HttpGet]
        [Route("showId")]
        public IActionResult GetSeatShowByShowId(int showId)
        {
            if (!_showRepository.ShowExist(showId))
                return NotFound();

            var seatShows = _mapper.Map<List<SeatShowDto>>(_seatShowRepository.GetSeatShowByShowId(showId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(seatShows);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSeatShow([FromQuery]int showId, [FromQuery]int seatTypeId,[FromBody] SeatShowDto seatShowCreate)
        {
            if(!_showRepository.ShowExist(showId))
                return NotFound("Show Not Found!");
            if(!_seatTypeRepository.SeatTypeExist(seatTypeId))
                return NotFound("Seat Type Not Found!");
            if (seatShowCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var seatShowMap = _mapper.Map<SeatShow>(seatShowCreate);
            seatShowMap.Show = _showRepository.GetShowById(showId);
            //seatShowMap.SeatType = _seatTypeRepository.GetSeatTypeById(seatTypeId);


            if (!_seatShowRepository.CreateSeatShow(seatShowMap))
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
        public IActionResult UpdateSeatShow(int id, [FromBody] SeatShowDto updatedSeatShow)
        {
            if (!_seatShowRepository.SeatShowExist(id))
                return NotFound();
            if (updatedSeatShow == null)
                return BadRequest(ModelState);

            if (id != updatedSeatShow.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var seatShowMap = _mapper.Map<SeatShow>(updatedSeatShow);
            if (!_seatShowRepository.UpdateSeatShow(seatShowMap))
            {
                ModelState.AddModelError("", "Something went wrong updating SeatShow!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSeatShow(int id)
        {
            if (!_seatShowRepository.SeatShowExist(id))
            {
                return NotFound();
            }

            var seatShowToDelete = _seatShowRepository.GetSeatShowById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_seatShowRepository.DeleteSeatShow(seatShowToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting SeatShow!");
            }

            return NoContent();
        }
    }
}
