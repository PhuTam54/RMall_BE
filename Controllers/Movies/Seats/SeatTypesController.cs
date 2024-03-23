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
        [ProducesResponseType(200, Type = typeof(SeatType))]
        [ProducesResponseType(400)]
        public IActionResult GetSeatTypeById(int id)
        {
            if (!_seatTypeRepository.SeatTypeExist(id))
                return NotFound();

            var seatType = _mapper.Map<SeatTypeDto>(_seatTypeRepository.GetSeatTypeById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(seatType);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
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

        [HttpPut]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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

        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
