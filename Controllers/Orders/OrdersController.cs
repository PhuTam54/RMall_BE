using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Dto.OrdersDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces;
using RMall_BE.Interfaces.MovieInterfaces;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Models;
using RMall_BE.Models.Orders;
using RMall_BE.Repositories;
using RMall_BE.Repositories.OrderRepositories;

namespace RMall_BE.Controllers.Orders
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IShowRepository _showRepository;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository, IShowRepository showRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _showRepository = showRepository;
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {

            var orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetAllOrder());

            return Ok(orders);
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public IActionResult GetOrderById(int id)
        {
            if (!_orderRepository.OrderExist(id))
                return NotFound();

            var order = _mapper.Map<OrderDto>(_orderRepository.GetOrderById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder([FromQuery]int userId, [FromQuery]int showId,[FromBody] OrderDto orderCreate)
        {
            if (!_userRepository.UserExist(userId))
                return NotFound("User Not Found!");
            if(!_showRepository.ShowExist(showId))
                return NotFound("Show Not Found!");
            if (orderCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderCreate);
            orderMap.User = _userRepository.GetUserById(userId);
            orderMap.Show = _showRepository.GetShowById(showId);

            if (!_orderRepository.CreateOrder(orderMap))
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
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrder(int id, [FromBody] OrderDto updatedOrder)
        {
            if (!_orderRepository.OrderExist(id))
                return NotFound();
            if (updatedOrder == null)
                return BadRequest(ModelState);

            if (id != updatedOrder.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var orderMap = _mapper.Map<Order>(updatedOrder);
            if (!_orderRepository.UpdateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Order!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrder(int id)
        {
            if (!_orderRepository.OrderExist(id))
            {
                return NotFound();
            }

            var orderToDelete = _orderRepository.GetOrderById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderRepository.DeleteOrder(orderToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Order!");
            }

            return NoContent();
        }
    }
}
