using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto;
using RMall_BE.Interfaces;
using RMall_BE.Models;

namespace RMall_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public FoodController(IFoodRepository foodRepository, IMapper mapper)
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

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Food))]
        [ProducesResponseType(400)]
        public IActionResult GetFoodById(int id)
        {
            if (!_foodRepository.FoodExist(id))
                return NotFound();

            var food = _mapper.Map<FoodDto>(_foodRepository.GetFoodById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(food);
        }

  
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
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

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFood(int id, [FromBody] FoodDto updatedFood)
        {
            if (!_foodRepository.FoodExist(id))
                return NotFound();
            if (updatedFood == null)
                return BadRequest(ModelState);

            if (id != updatedFood.id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var foodMap = _mapper.Map<Food>(updatedFood);
            if (!_foodRepository.UpdateFood(foodMap))
            {
                ModelState.AddModelError("", "Something went wrong updating reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFood(int id)
        {
            if (!_foodRepository.FoodExist(id))
            {
                return NotFound();
            }

            var foodToDelete = _foodRepository.GetFoodById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_foodRepository.DeleteFood(foodToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }

            return NoContent();
        }
    }
}
