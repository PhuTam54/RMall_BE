using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto.OrdersDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces.OrderInterfaces;
using RMall_BE.Models.Orders;

namespace RMall_BE.Controllers.Orders
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderFoodController : ControllerBase
    {
        private readonly IOrderFoodRepository _orderFoodRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IFoodRepository _foodRepository;

        public OrderFoodController(IOrderFoodRepository orderFoodRepository, IMapper mapper, IOrderRepository orderRepository, IFoodRepository foodRepository)
        {
            _orderFoodRepository = orderFoodRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _foodRepository = foodRepository;
        }

        [HttpGet]
        public IActionResult GetAllOrderFood()
        {

            var orderFoods = _mapper.Map<List<OrderFoodDto>>(_orderFoodRepository.GetAllOrderFood());

            return Ok(orderFoods);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetOrderFoodById(int id)
        {
            if (!_orderFoodRepository.OrderFoodExist(id))
                return NotFound();

            var orderFood = _mapper.Map<OrderFoodDto>(_orderFoodRepository.GetOrderFoodById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderFood);
        }

        /// <summary>
        /// Create OrderFood
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderFoodCreate"></param>
        /// <remarks>
        /// "qty": 2,
        /// "price": 3
        /// </remarks>
        /// <returns></returns>
        /// 
        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Customer")]
        [HttpPost]
        public IActionResult CreateOrderFood([FromQuery] int orderId, [FromQuery] int foodId, [FromBody] OrderFoodDto orderFoodCreate)
        {
            if (!_orderRepository.OrderExist(orderId))
                return NotFound("Order Not Found");
            if (!_foodRepository.FoodExist(foodId))
                return NotFound("Food Not Found");
            if (orderFoodCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderFoodMap = _mapper.Map<OrderFood>(orderFoodCreate);
            orderFoodMap.Order_Id = orderId;
            orderFoodMap.Food_Id = foodId;
            orderFoodMap.Order = _orderRepository.GetOrderById(orderId);
            orderFoodMap.Food = _foodRepository.GetFoodById(foodId);

            if (!_orderFoodRepository.CreateOrderFood(orderFoodMap))
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
        public IActionResult UpdateOrderFood(int id, [FromBody] OrderFoodDto updatedOrderFood)
        {
            if (!_orderFoodRepository.OrderFoodExist(id))
                return NotFound();
            if (updatedOrderFood == null)
                return BadRequest(ModelState);

            if (id != updatedOrderFood.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var orderFoodMap = _mapper.Map<OrderFood>(updatedOrderFood);
            if (!_orderFoodRepository.UpdateOrderFood(orderFoodMap))
            {
                ModelState.AddModelError("", "Something went wrong updating OrderFood!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteOrderFood(int id)
        {
            if (!_orderFoodRepository.OrderFoodExist(id))
            {
                return NotFound();
            }

            var orderFoodToDelete = _orderFoodRepository.GetOrderFoodById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderFoodRepository.DeleteOrderFood(orderFoodToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting OrderFood!");
            }

            return NoContent();
        }
    }
}
