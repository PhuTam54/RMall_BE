using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Dto.OrdersDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces;
using RMall_BE.Interfaces.MovieInterfaces.SeatInterfaces;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Movies;
using RMall_BE.Models.Orders;
using RMall_BE.Models.User;
using RMall_BE.Repositories.MovieRepositories;
using RMall_BE.Repositories.OrderRepositories;

namespace RMall_BE.Controllers.Orders
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderTicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ISeatRepository _seatRepository;

        public OrderTicketController(ITicketRepository ticketRepository, IMapper mapper, IOrderRepository orderRepository, ISeatRepository seatRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _seatRepository = seatRepository;
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
        /// <param name="seatId"></param>
        /// <param name="ticketCreate"></param>
        /// <remarks>
        /// "code": "ThisIsACode"
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateTicket([FromQuery] int orderId, [FromQuery] int seatId, [FromBody] TicketDto ticketCreate)
        {
            if (!_orderRepository.OrderExist(orderId))
                return NotFound("Order Not Found");
            if (!_seatRepository.SeatExist(seatId))
                return NotFound("Seat Not Found");
            if (ticketCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticketMap = _mapper.Map<Ticket>(ticketCreate);
            ticketMap.Order_Id = orderId;
            ticketMap.Seat_Id = seatId;
            ticketMap.Order = _orderRepository.GetOrderById(orderId);
            ticketMap.Seat = _seatRepository.GetSeatById(seatId);

            if (!_ticketRepository.CreateTicket(ticketMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
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
