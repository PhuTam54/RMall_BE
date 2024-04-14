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
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;

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
        [Route("OrderCode")]
        public IActionResult GetOrderByOrderCode(string orderCode)
        {
            if (!_orderRepository.OrderExist(orderCode))
                return NotFound();

            var order = _mapper.Map<OrderDto>(_orderRepository.GetOrderByOrderCode(orderCode));

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
        /// "order_Code": "ThisIsACodeUnique_1712599828518_0.1532859899449781",
        /// "total": 120,
        /// "discount_Amount": 1,
        /// "discount_Code": "thisIsADiscountCode",
        /// "final_Total": 119,
        /// "status": 0,
        /// "payment_Method": "vnpay",
        /// "is_Paid": false,
        /// "qR_Code": "ThisIsACodeUnique_1712599828518_0.1532859899449781",
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

            // Create a QR code writer instance
            var qrCodeWriter = new BarcodeWriter();
            qrCodeWriter.Format = BarcodeFormat.QR_CODE;
            qrCodeWriter.Options = new EncodingOptions
            {
                Width = 300, // Set the desired width of the QR code image
                Height = 300, // Set the desired height of the QR code image
                Margin = 0 // Set the margin of the QR code image
            };

            // Create a renderer instance
            var renderer = new ZXing.Rendering.BitmapRenderer();

            // Set the renderer instance for BarcodeWriter
            qrCodeWriter.Renderer = renderer;

            var qrCodeBitmap = qrCodeWriter.Write(orderCreate.QR_Code);

            // Convert QR code bitmap to base64 string
            var qrCodeBase64 = Convert.ToBase64String(BitmapToBytes(qrCodeBitmap));

            var orderMap = _mapper.Map<Order>(orderCreate);
            orderMap.User_Id = userId;
            orderMap.Show_Id = showId;
            orderMap.User = _userRepository.GetUserById(userId);
            orderMap.Show = _showRepository.GetShowById(showId);
            orderMap.QR_Code = qrCodeBase64;

            //var qrWriter = new ZXing.BarcodeWriterPixelData
            //{
            //    Format = ZXing.BarcodeFormat.QR_CODE,
            //    Options = new ZXing.QrCode.QrCodeEncodingOptions
            //    {
            //        Height = 200,
            //        Width = 200
            //    }
            //};

            //var pixelData = qrWriter.Write(orderCreate.QR_Code);
            //var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);
            //using (var ms = new MemoryStream(pixelData.Pixels))
            //{
            //    bitmap.Save(ms, ImageFormat.Png);
            //    var image = Image.FromStream(ms);
            //    // Now you can display the image in your UI
            //}

            if (!_orderRepository.CreateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

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

        [HttpGet]
        [Route("BitmapToBytes")]
        private byte[] BitmapToBytes(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
