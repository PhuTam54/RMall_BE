using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto.ShopsDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces.ShopInterfaces;
using RMall_BE.Models.Shops;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers.Shops
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FloorsController : Controller
    {
        private readonly IFloorRepository _floorRepository;
        private readonly IMapper _mapper;

        public FloorsController(IFloorRepository floorRepository, IMapper mapper)
        {
            _floorRepository = floorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllFloor()
        {

            var floors = _mapper.Map<List<FloorDto>>(_floorRepository.GetAllFloor());
            return Ok(floors);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetFloorById(int id)
        {
            if (!_floorRepository.FloorExist(id))
                return NotFound();

            var floor = _mapper.Map<FloorDto>(_floorRepository.GetFloorById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(floor);
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateFloor([FromBody] FloorDto floorCreate)
        {
            if (floorCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var floorMap = _mapper.Map<Floor>(floorCreate);


            if (!_floorRepository.CreateFloor(floorMap))
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
        public IActionResult UpdateFloor(int id, [FromBody] FloorDto updatedFloor)
        {
            if (!_floorRepository.FloorExist(id))
                return NotFound();
            if (updatedFloor == null)
                return BadRequest(ModelState);

            if (id != updatedFloor.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var floorMap = _mapper.Map<Floor>(updatedFloor);
            if (!_floorRepository.UpdateFloor(floorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating floor");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteFloor(int id)
        {
            if (!_floorRepository.FloorExist(id))
            {
                return NotFound();
            }

            var floorToDelete = _floorRepository.GetFloorById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_floorRepository.DeleteFloor(floorToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting floor");
            }

            return NoContent();
        }
    }
}
