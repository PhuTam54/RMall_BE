﻿using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Data;
using RMall_BE.Dto.ShopsDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces.ShopInterfaces;
using RMall_BE.Models.Shops;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers.Shops
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShopsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFloorRepository _floorRepository;

        public ShopsController(IMapper mapper, IShopRepository shopRepository, ICategoryRepository categoryRepository, IFloorRepository floorRepository)
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
        [Route("floor")]
        public IActionResult GetShopByFloorId(int floorId)
        {
            if (!_floorRepository.FloorExist(floorId))
                return NotFound();

            var shop = _mapper.Map<List<ShopDto>>(_shopRepository.GetShopByFloorId(floorId));

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
        /// {
        /// "name": "Gucci",
        /// "image": "Gucci/img",
        /// "address": "145A",
        /// "phone_Number": "0987654321",
        /// "description": "Gucci in you area"
        /// }
        /// 
        /// </remarks>
        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateShop([FromQuery] int categoryId, [FromQuery] int floorId, [FromBody] ShopDto shopCreate)
        {
            if (categoryId == null && floorId == null)
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
            shopMap.Category_Id = categoryId;
            shopMap.Floor_Id = floorId;
            shopMap.Category = _categoryRepository.GetCategoryById(categoryId);
            shopMap.Floor = _floorRepository.GetFloorById(floorId);

            if (!_shopRepository.CreateShop(shopMap))
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
        public IActionResult UpdateShop(int id, [FromBody] ShopDto updatedShop)
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

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
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
