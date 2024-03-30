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
    public class SeatPricingsController : ControllerBase
    {
        private readonly ISeatPricingRepository _seatPricingRepository;
        private readonly IMapper _mapper;
        private readonly IShowRepository _showRepository;
        private readonly ISeatTypeRepository _seatTypeRepository;

        public SeatPricingsController(ISeatPricingRepository seatPricingRepository, IMapper mapper, IShowRepository showRepository, ISeatTypeRepository seatTypeRepository)
        {
            _seatPricingRepository = seatPricingRepository;
            _mapper = mapper;
            _showRepository = showRepository;
            _seatTypeRepository = seatTypeRepository;
        }

        [HttpGet]
        public IActionResult GetAllSeatPricing()
        {

            var seatPricings = _mapper.Map<List<SeatPricingDto>>(_seatPricingRepository.GetAllSeatPricing());

            return Ok(seatPricings);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetSeatPricingById(int id)
        {
            if (!_seatPricingRepository.SeatPricingExist(id))
                return NotFound();

            var seatPricing = _mapper.Map<SeatPricingDto>(_seatPricingRepository.GetSeatPricingById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(seatPricing);
        }

        [HttpGet]
        [Route("showId")]
        public IActionResult GetSeatPricingByShowId(int showId)
        {
            if (!_showRepository.ShowExist(showId))
                return NotFound();

            var seatPricings = _mapper.Map<List<SeatPricingDto>>(_seatPricingRepository.GetSeatPricingByShowId(showId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(seatPricings);
        }

        [HttpPost]
        public IActionResult CreateSeatPricing([FromQuery]int showId, [FromQuery]int seatTypeId,[FromBody] SeatPricingDto seatPricingCreate)
        {
            if(!_showRepository.ShowExist(showId))
                return NotFound("Show Not Found!");
            if(!_seatTypeRepository.SeatTypeExist(seatTypeId))
                return NotFound("Seat Type Not Found!");
            if (seatPricingCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var seatPricingMap = _mapper.Map<SeatPricing>(seatPricingCreate);
            seatPricingMap.Show = _showRepository.GetShowById(showId);
            seatPricingMap.SeatType = _seatTypeRepository.GetSeatTypeById(seatTypeId);


            if (!_seatPricingRepository.CreateSeatPricing(seatPricingMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("id")]
        public IActionResult UpdateSeatPricing(int id, [FromBody] SeatPricingDto updatedSeatPricing)
        {
            if (!_seatPricingRepository.SeatPricingExist(id))
                return NotFound();
            if (updatedSeatPricing == null)
                return BadRequest(ModelState);

            if (id != updatedSeatPricing.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var seatPricingMap = _mapper.Map<SeatPricing>(updatedSeatPricing);
            if (!_seatPricingRepository.UpdateSeatPricing(seatPricingMap))
            {
                ModelState.AddModelError("", "Something went wrong updating SeatPricing!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteSeatPricing(int id)
        {
            if (!_seatPricingRepository.SeatPricingExist(id))
            {
                return NotFound();
            }

            var seatPricingToDelete = _seatPricingRepository.GetSeatPricingById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_seatPricingRepository.DeleteSeatPricing(seatPricingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting SeatPricing!");
            }

            return NoContent();
        }
    }
}
