using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Helpers;
using RMall_BE.Identity;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Models.Movies;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers.Movies
{
    [Route("api/v1/[controller]")]
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
        public IActionResult GetRoomByName([FromQuery] string name)
        {
            var room = _roomRepository.GetRoomByName(name);
            if (room == null)
                return NotFound();

            var roomMap = _mapper.Map<RoomDto>(room);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(roomMap);
        }


        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateRoom([FromBody] RoomDto roomCreate)
        {
            if (roomCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roomMap = _mapper.Map<Room>(roomCreate);
            roomMap.Slug = CreateSlug.Init_Slug(roomCreate.Name);

            if (!_roomRepository.CreateRoom(roomMap))
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
            roomMap.Slug = CreateSlug.Init_Slug(updatedRoom.Name);

            if (!_roomRepository.UpdateRoom(roomMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Room!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
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
                ModelState.AddModelError("", "Something went wrong deleting Room!");
            }

            return NoContent();
        }
    }
}
