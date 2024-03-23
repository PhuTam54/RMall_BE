using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Dto.MoviesDto.SeatsDto;
using RMall_BE.Interfaces;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies.Seats;
using RMall_BE.Repositories.MovieRepositories.SeatRepositories;

namespace RMall_BE.Controllers.Movies.Seats
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeatReservationsController : Controller
    {
        private readonly ISeatReservationRepository _seatReservationRepository;
        private readonly IMapper _mapper;

        public SeatReservationsController(ISeatReservationRepository seatReservationRepository, IMapper mapper)
        {
            _seatReservationRepository = seatReservationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllSeatReservation()
        {

            var seatReservations = _mapper.Map<List<SeatReservationDto>>(_seatReservationRepository.GetAllSeatReservation());

            return Ok(seatReservations);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(SeatReservation))]
        [ProducesResponseType(400)]
        public IActionResult GetSeatReservationById(int id)
        {
            if (!_seatReservationRepository.SeatReservationExist(id))
                return NotFound();

            var seatReservation = _mapper.Map<SeatReservationDto>(_seatReservationRepository.GetSeatReservationById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(seatReservation);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSeatReservation([FromBody] SeatReservationDto seatReservationCreate)
        {
            if (seatReservationCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var seatReservationMap = _mapper.Map<SeatReservation>(seatReservationCreate);


            if (!_seatReservationRepository.CreateSeatReservation(seatReservationMap))
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
        public IActionResult UpdateSeatReservation(int id, [FromBody] SeatReservationDto updatedSeatReservation)
        {
            if (!_seatReservationRepository.SeatReservationExist(id))
                return NotFound();
            if (updatedSeatReservation == null)
                return BadRequest(ModelState);

            if (id != updatedSeatReservation.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var seatReservationMap = _mapper.Map<SeatReservation>(updatedSeatReservation);
            if (!_seatReservationRepository.UpdateSeatReservation(seatReservationMap))
            {
                ModelState.AddModelError("", "Something went wrong updating SeatReservation!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSeatReservation(int id)
        {
            if (!_seatReservationRepository.SeatReservationExist(id))
            {
                return NotFound();
            }

            var seatReservationToDelete = _seatReservationRepository.GetSeatReservationById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_seatReservationRepository.DeleteSeatReservation(seatReservationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting SeatReservation!");
            }

            return NoContent();
        }
    }
}
