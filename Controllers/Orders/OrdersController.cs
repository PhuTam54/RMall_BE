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
using RMall_BE.Models.Orders;
using RMall_BE.Models.User;

namespace RMall_BE.Controllers.Orders
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository<Customer> _userRepository;
        private readonly IShowRepository _showRepository;
        private readonly IFoodRepository _foodRepository;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper, IUserRepository<Customer> userRepository, IShowRepository showRepository, IFoodRepository foodRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _showRepository = showRepository;
            _foodRepository = foodRepository;
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {

            var orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetAllOrder());

            return Ok(orders);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetOrderById(int id)
        {
            if (!_orderRepository.OrderExist(id))
                return NotFound();

            var order = _mapper.Map<OrderDto>(_orderRepository.GetOrderById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);
        }

        [HttpGet]
        [Route("userId")]
        public IActionResult GetOrderByUserId(int userId)
        {
            if (!_userRepository.UserExist(userId))
                return NotFound("User Not Found!");

            var orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetOrderByUserId(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="showId"></param>
        /// <param name="orderCreate"></param>
        /// <remarks>
        /// "order_Code": "abcdxyzThisIsACode",
        /// "total": 23456,
        /// "discount_Amount": 11111,
        /// "discount_Code": "321paCgnaDnauQzyxdcba",
        /// "final_Total": 12345,
        /// "payment_Method": "momo",
        /// "qR_Code": "octimuspraise",
        /// </remarks>
        /// <returns></returns>
        /// 
        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Customer")]
        [HttpPost]
        public IActionResult CreateOrder([FromQuery] int userId, [FromQuery] int showId, [FromBody] OrderDto orderCreate)
        {
            if (!_userRepository.UserExist(userId))
                return NotFound("User Not Found!");
            if (!_showRepository.ShowExist(showId))
                return NotFound("Show Not Found!");
            if (orderCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderCreate);
            orderMap.User_Id = userId;
            orderMap.Show_Id = showId;
            orderMap.User = _userRepository.GetUserById(userId);
            orderMap.Show = _showRepository.GetShowById(showId);

            if (!_orderRepository.CreateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            //var foods = new List<Food>();
            //foreach (var foodId in orderCreate.FoodIds)
            //{
            //    Food food = _foodRepository.GetFoodById(foodId);
            //    if (food == null)
            //    {
            //        return NotFound("Food Not Found!");
            //    }
            //    foods.Add(food);
            //}
            //foreach (var item in foods)
            //{
            //    var orderFood = new OrderFood { Order_Id = orderMap.Id, Food_Id = item.Id, Order = orderMap, Food = item, Price = 1, Qty = 1 };
            //    _orderRepository.CreateOrderFood(orderFood);
            //}

            //var ticket = new Ticket { Order_Id = orderMap.Id, Is_Used = false, Seat_Id = 0 };
            //_orderRepository.CreateTicket(ticket);

            return Created("Order created successfully", orderCreate);
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPut]
        [Route("id")]
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
