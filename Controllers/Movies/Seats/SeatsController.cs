using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMall_BE.Data;
using RMall_BE.Dto.MoviesDto.SeatsDto;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Models.Movies.Seats;
using RMall_BE.Repositories.MovieRepositories;

namespace RMall_BE.Controllers.Movies.Seats
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : Controller
    {
        private readonly RMallContext _context;
        private readonly ISeatRepository _seatRepository;
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;

        public SeatsController(RMallContext context ,ISeatRepository seatRepository, IMapper mapper,IRoomRepository roomRepository)
        {
            _context = context;
            _seatRepository = seatRepository;
            _mapper = mapper;
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetAllSeat()
        {

            var seats = _mapper.Map<List<SeatDto>>(_seatRepository.GetAllSeat());

            return Ok(seats);
        }

        [HttpGet]
        [Route("id")]
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
        public IActionResult CreateSeat([FromQuery]int roomId, [FromQuery]int seatTypeId, [FromQuery]int seatReservationId ,[FromBody] SeatDto seatCreate)
        {
            if (!_roomRepository.RoomExist(roomId))
                return NotFound("Room Not Found!");
            var seatType = _context.SeatTypes.FirstOrDefault(st => st.Id == seatTypeId);
            var seatReservation = _context.SeatReservations.FirstOrDefault(sr => sr.Id == seatReservationId);
            if (seatType == null)
                return NotFound("Set Type Not Found!");
            if(seatReservation == null)
                return NotFound("Set Reservation Not Found!");

            if (seatCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var seatMap = _mapper.Map<Seat>(seatCreate);
            seatMap.Room = _roomRepository.GetRoomById(roomId);
            seatMap.SeatType = seatType;
            seatMap.SeatReservation = seatReservation;

            if (!_seatRepository.CreateSeat(seatMap))
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
        public IActionResult UpdateSeat(int id, [FromBody] SeatDto updatedSeat)
        {
            if (!_seatRepository.SeatExist(id))
                return NotFound();
            if (updatedSeat == null)
                return BadRequest(ModelState);

            if (id != updatedSeat.Id)
                return BadRequest(ModelState);


            var seatMap = _mapper.Map<Seat>(updatedSeat);
            if (!_seatRepository.UpdateSeat(seatMap))
            {
                ModelState.AddModelError("", "Something went wrong updating seat");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
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


            if (!_seatRepository.DeleteSeat(seatToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting seat");
            }

            return NoContent();
        }
    }
}
