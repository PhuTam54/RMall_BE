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
    public class FoodsController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public FoodsController(IFoodRepository foodRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllFood()
        {
            var foods = _mapper.Map<List<FoodDto>>(_foodRepository.GetAllFood());

            return Ok(foods);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetFoodById(int id)
        {
            if (!_foodRepository.FoodExist(id))
                return NotFound();

            var food = _mapper.Map<FoodDto>(_foodRepository.GetFoodById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(food);
        }

        /// <summary>
        /// Create Food
        /// </summary>
        /// <param name="foodCreate"></param>
        /// <remarks>
        /// "name": "Popcorn",
        /// "qty": "89",
        /// "price": "3",
        /// "description": "Popcorn is sweet"
        /// </remarks>
        /// <returns></returns>
        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateFood([FromBody] FoodDto foodCreate)
        {
            if (foodCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var foodMap = _mapper.Map<Food>(foodCreate);

            if (!_foodRepository.CreateFood(foodMap))
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
        public IActionResult UpdateFood(int id, [FromBody] FoodDto updatedFood)
        {
            if (!_foodRepository.FoodExist(id))
                return NotFound();
            if (updatedFood == null)
                return BadRequest(ModelState);

            if (id != updatedFood.Id)
                return BadRequest(ModelState);


            var foodMap = _mapper.Map<Food>(updatedFood);
            if (!_foodRepository.UpdateFood(foodMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Food!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteFood(int id)
        {
            if (!_foodRepository.FoodExist(id))
            {
                return NotFound();
            }

            var foodToDelete = _foodRepository.GetFoodById(id);

            if (!_foodRepository.DeleteFood(foodToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Food!");
            }

            return NoContent();
        }
    }
}
