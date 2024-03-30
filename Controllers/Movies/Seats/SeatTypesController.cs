using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Dto.MoviesDto.SeatsDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies.Seats;
using RMall_BE.Repositories.MovieRepositories.SeatRepositories;

namespace RMall_BE.Controllers.Movies.Seats
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeatTypesController : Controller
    {
        private readonly ISeatTypeRepository _seatTypeRepository;
        private readonly IMapper _mapper;

        public SeatTypesController(ISeatTypeRepository seatTypeRepository, IMapper mapper)
        {
            _seatTypeRepository = seatTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllSeatType()
        {

            var seatTypes = _mapper.Map<List<SeatTypeDto>>(_seatTypeRepository.GetAllSeatType());

            return Ok(seatTypes);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetSeatTypeById(int id)
        {
            if (!_seatTypeRepository.SeatTypeExist(id))
                return NotFound();

            var seatType = _mapper.Map<SeatTypeDto>(_seatTypeRepository.GetSeatTypeById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(seatType);
        }

        /// <summary>
        /// Create SeatType
        /// </summary>
        /// <param name="seatTypeCreate"></param>
        /// <remarks>
        /// "name": "VIP, Regular, double"
        /// </remarks>
        /// <returns></returns>
        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateSeatType([FromBody] SeatTypeDto seatTypeCreate)
        {
            
            if (seatTypeCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var seatTypeMap = _mapper.Map<SeatType>(seatTypeCreate);


            if (!_seatTypeRepository.CreateSeatType(seatTypeMap))
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
        public IActionResult UpdateSeatType(int id, [FromBody] SeatTypeDto updatedSeatType)
        {
            if (!_seatTypeRepository.SeatTypeExist(id))
                return NotFound();
            if (updatedSeatType == null)
                return BadRequest(ModelState);

            if (id != updatedSeatType.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var seatTypeMap = _mapper.Map<SeatType>(updatedSeatType);
            if (!_seatTypeRepository.UpdateSeatType(seatTypeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating SeatType!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteSeatType(int id)
        {
            if (!_seatTypeRepository.SeatTypeExist(id))
            {
                return NotFound();
            }

            var seatTypeToDelete = _seatTypeRepository.GetSeatTypeById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_seatTypeRepository.DeleteSeatType(seatTypeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting SeatType!");
            }

            return NoContent();
        }
    }
}
