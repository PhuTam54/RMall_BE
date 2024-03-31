using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMall_BE.Dto.ShopsDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces.ShopInterfaces;
using RMall_BE.Models.Shops;
using RMall_BE.Repositories;

namespace RMall_BE.Controllers.Shops
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;

        public ProductsController(IProductRepository productRepository, IMapper mapper, IShopRepository shopRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _shopRepository = shopRepository;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetAllProduct());
            return Ok(products);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetProductById([FromQuery] int id)
        {
            if (!_productRepository.ProductExist(id))
                return NotFound();
            return Ok(_mapper.Map<ProductDto>(_productRepository.GetProductById(id)));
        }
        [HttpGet]
        [Route("name")]
        public IActionResult GetProductByName([FromQuery] string name)
        {
            var products = _productRepository.GetProductByName(name);
            if (products == null)
                return NotFound();
            return Ok(_mapper.Map<ProductDto>(products));
        }
        /// <summary>
        /// Create PRoduct
        /// </summary>
        /// <param name="productCreate"></param>
        /// <returns></returns>
        /// <remarks>
        /// "name": "Gucci bag",
        /// "image": "guccibag/img",
        /// "price": 329,
        /// "description": "Gucci bag"
        /// </remarks>

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPost]
        public IActionResult CreateProduct([FromQuery] int shopId, [FromBody] ProductDto productCreate)
        {
            if (!_shopRepository.ShopExist(shopId))
                return NotFound("Shop Not Found!");
            if (productCreate == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();
            var productMap = _mapper.Map<Product>(productCreate);
            productMap.Shop = _shopRepository.GetShopById(shopId);
            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Create Product Successfully!");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPut]
        [Route("id")]
        public IActionResult UpdateProduct([FromQuery] int id, [FromBody] ProductDto productUpdate)
        {
            if (!_productRepository.ProductExist(id))
                return NotFound();

            if (id != productUpdate.Id)
                return BadRequest();


            var productMap = _mapper.Map<Product>(productUpdate);

            if (!_productRepository.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong updating product");
                return StatusCode(500, ModelState);
            }
            return Ok("Update Product Successfully!");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            if (!_productRepository.ProductExist(id))
                return NotFound();

            var productToDelete = _productRepository.GetProductById(id);



            if (!_productRepository.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
                return StatusCode(500, ModelState);
            }
            return Ok("Delete Product Successfully!");
        }
    }
}
