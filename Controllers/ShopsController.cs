using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Data;
using RMall_BE.Dto;
using RMall_BE.Interfaces;
using RMall_BE.Models;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFloorRepository _floorRepository;

        public ShopsController(IMapper mapper, IShopRepository shopRepository, ICategoryRepository categoryRepository,IFloorRepository floorRepository)
        {
            _mapper = mapper;
            _shopRepository = shopRepository;
            _categoryRepository = categoryRepository;
            _floorRepository = floorRepository;
        }

        [HttpGet]
        public IActionResult GetAllShop()
        {
            var shops = _mapper.Map<List<ShopDto>>(_shopRepository.GetAllShop());
            return Ok(shops);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetShopById(int id)
        {
            if (!_shopRepository.ShopExist(id))
                return NotFound();

            var shop = _mapper.Map<ShopDto>(_shopRepository.GetShopById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shop);
        }

        [HttpGet]
        [Route("name")]
        public IActionResult GetShopByName(string name)
        {
            var shop = _shopRepository.GetShopByName(name);
            if (shop == null)
                return NotFound();

            var shopMap = _mapper.Map<ShopDto>(shop);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shopMap);
        }

        /// <summary>
        /// Create Shop
        /// </summary>
        /// <param name="shopCreate"></param>
        /// <returns></returns>
        /// <remarks>
        /// POST/Shop
        /// {
        /// "name": "HuuQuanShop",
        /// "image": "HuuQuanShop/img",
        /// "address": "Ha Noi",
        /// "phone_Number": "0987654321",
        /// "description": "HuuQuanShop Dang Cap So 1 The Gioi!"
        /// }
        /// 
        /// </remarks>
        [HttpPost]
        public IActionResult CreateShop([FromQuery]int categoryId, [FromQuery] int floorId, [FromBody]ShopDto shopCreate)
        {
            if(categoryId == null && floorId == null)
                return BadRequest(ModelState);
            if (!_categoryRepository.CategoryExist(categoryId))
                return NotFound("Category Not Found!");
            if (!_floorRepository.FloorExist(floorId))
                return NotFound("Floor Not Found!");
            if (shopCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shopMap = _mapper.Map<Shop>(shopCreate);
            shopMap.Category = _categoryRepository.GetCategoryById(categoryId);
            shopMap.Floor = _floorRepository.GetFloorById(floorId);

            if (!_shopRepository.CreateShop(shopMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("id")]
        public IActionResult UpdateShop(int id, [FromBody]ShopDto updatedShop)
        {
            if (!_shopRepository.ShopExist(id))
                return NotFound();
            if (updatedShop == null)
                return BadRequest(ModelState);

            if (id != updatedShop.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var shopMap = _mapper.Map<Shop>(updatedShop);
            if (!_shopRepository.UpdateShop(shopMap))
            {
                ModelState.AddModelError("", "Something went wrong updating shop");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteShop(int id)
        {
            if (!_shopRepository.ShopExist(id))
            {
                return NotFound();
            }

            var shopToDelete = _shopRepository.GetShopById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_shopRepository.DeleteShop(shopToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting shop");
            }

            return NoContent();
        }
    }
}
