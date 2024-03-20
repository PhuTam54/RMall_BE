using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Interfaces;
using RMall_BE.Models;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomsController(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllRoom()
        {

            var rooms = _mapper.Map<List<RoomDto>>(_roomRepository.GetAllRoom());

            return Ok(rooms);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(Room))]
        [ProducesResponseType(400)]
        public IActionResult GetRoomById([FromQuery] int id)
        {
            if (!_roomRepository.RoomExist(id))
                return NotFound();

            var room = _mapper.Map<RoomDto>(_roomRepository.GetRoomById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(room);
        }

        [HttpGet]
        [Route("name")]
        [ProducesResponseType(200, Type = typeof(Room))]
        [ProducesResponseType(400)]
        public IActionResult GetRoomByName([FromQuery]string name)
        {
            var room = _roomRepository.GetRoomByName(name);
            if (room == null)
                return NotFound();

            var roomMap = _mapper.Map<RoomDto>(room);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(roomMap);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRoom([FromBody] RoomDto roomCreate)
        {
            if (roomCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roomMap = _mapper.Map<Room>(roomCreate);


            if (!_roomRepository.CreateRoom(roomMap))
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
        public IActionResult UpdateRoom([FromQuery] int id, [FromBody] RoomDto updatedRoom)
        {
            if (!_roomRepository.RoomExist(id))
                return NotFound();
            if (updatedRoom == null)
                return BadRequest(ModelState);

            if (id != updatedRoom.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var roomMap = _mapper.Map<Room>(updatedRoom);
            if (!_roomRepository.UpdateRoom(roomMap))
            {
                ModelState.AddModelError("", "Something went wrong updating reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRoom([FromQuery] int id)
        {
            if (!_roomRepository.RoomExist(id))
            {
                return NotFound();
            }

            var roomToDelete = _roomRepository.GetRoomById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_roomRepository.DeleteRoom(roomToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }

            return NoContent();
        }
    }
}
