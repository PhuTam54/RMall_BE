﻿using AutoMapper;
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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeatReservationsController : Controller
    {
        private readonly ISeatReservationRepository _seatReservationRepository;
        private readonly IMapper _mapper;
        private readonly ISeatRepository _seatRepository;
        private readonly IShowRepository _showRepository;

        public SeatReservationsController(ISeatReservationRepository seatReservationRepository, IMapper mapper, ISeatRepository seatRepository, IShowRepository showRepository)
        {
            _seatReservationRepository = seatReservationRepository;
            _mapper = mapper;
            _seatRepository = seatRepository;
            _showRepository = showRepository;
        }

        [HttpGet]
        public IActionResult GetAllSeatReservation()
        {

            var seatReservations = _mapper.Map<List<SeatReservationDto>>(_seatReservationRepository.GetAllSeatReservation());

            return Ok(seatReservations);
        }

        [HttpGet]
        [Route("id")]
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
        public IActionResult CreateSeatReservation([FromQuery] int seatId, [FromQuery] int showId, [FromBody] SeatReservationDto seatReservationCreate)
        {
            if (!_seatRepository.SeatExist(seatId))
                return NotFound("Seat Not Found");
            if (!_showRepository.ShowExist(showId))
                return NotFound("Show Not Found");
            if (seatReservationCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var seatReservationMap = _mapper.Map<SeatReservation>(seatReservationCreate);
            seatReservationMap.Seat = _seatRepository.GetSeatById(seatId);
            seatReservationMap.Seat_Id = seatId;
            seatReservationMap.Show = _showRepository.GetShowById(showId);
            seatReservationMap.Show_Id = showId;

            if (!_seatReservationRepository.CreateSeatReservation(seatReservationMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            var seatReservation = _mapper.Map<SeatReservationDto>(seatReservationMap);

            return Ok(seatReservation);
        }

        [HttpPut]
        [Route("id")]
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
