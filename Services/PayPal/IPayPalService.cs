using RMall_BE.Dto.PayPal;

namespace RMall_BE.Services.PayPal;
public interface IPayPalService
{
    Task<string> CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}