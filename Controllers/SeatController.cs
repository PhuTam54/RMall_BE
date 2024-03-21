using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : Controller
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IMapper _mapper;

        public SeatController(ISeatRepository seatRepository, IMapper mapper)
        {
            _seatRepository = seatRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllSeat()
        {

            var seats = _mapper.Map<List<SeatDto>>(_seatRepository.GetAllSeat());

            return Ok(seats);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Seat))]
        [ProducesResponseType(400)]
        public IActionResult GetSeatById(int id)
        {
            if (!_seatRepository.SeatExist(id))
                return NotFound();
            var movie = _mapper.Map<SeatDto>(_seatRepository.GetSeatById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(movie);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSeat([FromBody] SeatDto seatCreate)
        { 
            if (seatCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var seatMap = _mapper.Map<Seat>(seatCreate);


            if (!_seatRepository.CreateSeat(seatMap))
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
        public IActionResult UpdateSeat(int id, [FromBody] SeatDto updatedSeat)
        {
            if (!_seatRepository.SeatExist(id))
                return NotFound();
            if (updatedSeat == null)
                return BadRequest(ModelState);

            if (id != updatedSeat.id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var seatMap = _mapper.Map<Seat>(updatedSeat);
            if (!_seatRepository.UpdateSeat(seatMap))
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
        public IActionResult DeleteSeat(int id)
        {
            if (!_seatRepository.SeatExist(id))
            {
                return NotFound();
            }

            var seatToDelete = _seatRepository.GetSeatById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_seatRepository.DeleteSeat(seatToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }

            return NoContent();
        }
    }
}
