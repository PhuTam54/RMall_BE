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

        public ShopsController(IMapper mapper, IShopRepository shopRepository)
        {
            _mapper = mapper;
            _shopRepository = shopRepository;
        }

        [HttpGet]
        public IActionResult GetAllShop()
        {
            var shops = _mapper.Map<List<ShopDto>>(_shopRepository.GetAllShop());
            return Ok(shops);
        }

        [HttpGet("{id}")]
        public IActionResult GetShop(int id)
        {
            if (!_shopRepository.ShopExist(id))
                return NotFound();

            var shop = _mapper.Map<ShopDto>(_shopRepository.GetShopById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shop);
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
        public IActionResult CreateShop([FromBody]ShopDto shopCreate)
        {
            if (shopCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shopMap = _mapper.Map<Shop>(shopCreate);


            if (!_shopRepository.CreateShop(shopMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
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
                ModelState.AddModelError("", "Something went wrong updating reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
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
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            }

            return NoContent();
        }
    }
}
