
using RMall_BE.Dto.VNPay;

namespace RMall_BE.Services.VNPay
{
    public interface IVnPay
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExcute(IQueryCollection collections);
    }
}
