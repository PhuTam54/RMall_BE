using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RMall_BE.Data;
using RMall_BE.Dto.ShopsDto;
using RMall_BE.Identity;
using RMall_BE.Interfaces.ShopInterfaces;
using RMall_BE.Models.Shops;
using RMall_BE.Repositories;
using System.Linq;

namespace RMall_BE.Controllers.Shops
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IShopRepository _shopRepository;
        private readonly RMallContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public ProductsController(IProductRepository productRepository, IMapper mapper, IShopRepository shopRepository, RMallContext context, IWebHostEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _shopRepository = shopRepository;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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
        public async Task<IActionResult> CreateProduct([FromQuery] int shopId, [FromForm] ProductDto2 productCreate)
        {
            if (shopId <= 0)
            {
                return BadRequest("Invalid shop ID.");
            }

            if (productCreate == null)
            {
                return BadRequest("Product data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productCreate.Image == null || productCreate.Image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(productCreate.Image.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Invalid file extension. Allowed extensions are: " + string.Join(", ", allowedExtensions));
            }

            if (productCreate.Image.Length > 5 * 1024 * 1024) // 5 MB
            {
                return BadRequest("File size should not exceed 5MB.");
            }

            var uploadFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadFolderPath);

            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productCreate.Image.CopyToAsync(stream);
            }

            var product = _mapper.Map<Product>(productCreate);
            product.Image = "/uploads/" + fileName;
            product.Shop_Id = shopId;
            product.Shop = _shopRepository.GetShopById(shopId);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok("Product created successfully!");
        }

        [Authorize]
        [RequiresClaim(IdentityData.RoleClaimName, "Admin")]
        [HttpPut]
        [Route("id")]
        public IActionResult UpdateProduct([FromQuery] int id, [FromBody] ProductDto productUpdate)
        {
            if (!_productRepository.ProductExist(id))
                return NotFound();

            //if (id != productUpdate.Id)
            //    return BadRequest();


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