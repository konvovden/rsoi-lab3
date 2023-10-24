using GatewayService.Server.Dto.Converters.Payment.Enums;
using ApiPayment = PaymentService.Api.Payment;
using DtoPayment = GatewayService.Dto.Payments.Payment;

namespace GatewayService.Server.Dto.Converters.Payment;

public static class PaymentConverter
{
    public static DtoPayment Convert(ApiPayment apiPayment)
    {
        return new DtoPayment(apiPayment.Id,
            PaymentStatusConverter.Convert(apiPayment.Status),
            apiPayment.Price);
    }
}