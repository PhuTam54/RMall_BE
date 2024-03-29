
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using RMall_BE.Dto.Momo;
using RMall_BE.Dto.PayPal;
using RMall_BE.Dto.VNPay;
using RMall_BE.Services.Momo;
using RMall_BE.Services.PayPal;
using RMall_BE.Services.VNPay;


namespace RMall_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private IMomoService _momoService;
        private IVnPay _vnPay;
        private readonly IPayPalService _payPalService;

        public PaymentsController(IMomoService momoService, IVnPay vnPay, IPayPalService payPalService)
        {
            _momoService = momoService;
            _vnPay = vnPay;
            _payPalService = payPalService;
        }

        [HttpPost]
        [Route("Momo")]
        public async Task<IActionResult> Momo([FromBody]OrderInfoModel model)
        {
            var response = await _momoService.CreatePaymentAsync(model);
            return Ok(response.PayUrl);
        }

        //[HttpGet]
        //public IActionResult PaymentMomoCallBack()
        //{
        //    var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
        //    return Ok(response);
        //}

        [HttpPost]
        [Route("VnPay")]
        public IActionResult VnPay([FromBody] VnPaymentRequestModel vnPaymentRequestModel)
        {

            if (vnPaymentRequestModel == null)
                return BadRequest(ModelState);

            var paymentUrlString = _vnPay.CreatePaymentUrl(HttpContext, vnPaymentRequestModel);

            //var queryString = QueryHelpers.ParseQuery(new Uri(paymentUrlString).Query);
            //IQueryCollection createdVnpay = new QueryCollection(queryString);


            return Ok(paymentUrlString);
        }

        //[HttpGet]
        //public IActionResult PaymentVnPayCallBack()
        //{
        //    var response = _vnPay.PaymentExcute(Request.Query);
        //    if (response == null || response.VnPayResponseCode != "00")
        //    {
        //        return BadRequest("Thanh toan fail!");
        //    }
        //    return Ok(response);
        //}

        [HttpPost]
        [Route("PayPal")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] PaymentInformationModel model)
        {
            var url = await _payPalService.CreatePaymentUrl(model, HttpContext);

            return Ok(url);
        }



    }
}
