using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Dto.MoviesDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies;
using RMall_BE.Repositories.MovieRepositories;
using RMall_BE.Repositories.OrderRepositories;

namespace RMall_BE.Controllers.Movies
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketsController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public TicketsController(ITicketRepository ticketRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult GetAllTicket()
        {

            var tickets = _mapper.Map<List<TicketDto>>(_ticketRepository.GetAllTicket());

            return Ok(tickets);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetTicketById(int id)
        {
            if (!_ticketRepository.TicketExist(id))
                return NotFound();

            var ticket = _mapper.Map<TicketDto>(_ticketRepository.GetTicketById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ticket);
        }

        /// <summary>
        /// Create Ticket
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="ticketCreate"></param>
        /// <remarks>
        /// "code": "ThisIsACode"
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateTicket([FromQuery] int orderId, [FromBody] TicketDto ticketCreate)
        {
            if (!_orderRepository.OrderExist(orderId))
                return NotFound("Order Not Found");
            if (ticketCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticketMap = _mapper.Map<Ticket>(ticketCreate);
            ticketMap.Order = _orderRepository.GetOrderById(orderId);

            if (!_ticketRepository.CreateTicket(ticketMap))
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
        public IActionResult UpdateTicket(int id, [FromBody] TicketDto updatedTicket)
        {
            if (!_ticketRepository.TicketExist(id))
                return NotFound();
            if (updatedTicket == null)
                return BadRequest(ModelState);

            if (id != updatedTicket.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var ticketMap = _mapper.Map<Ticket>(updatedTicket);
            if (!_ticketRepository.UpdateTicket(ticketMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Ticket!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteTicket(int id)
        {
            if (!_ticketRepository.TicketExist(id))
            {
                return NotFound();
            }

            var ticketToDelete = _ticketRepository.GetTicketById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ticketRepository.DeleteTicket(ticketToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Ticket!");
            }

            return NoContent();
        }
    }
}
