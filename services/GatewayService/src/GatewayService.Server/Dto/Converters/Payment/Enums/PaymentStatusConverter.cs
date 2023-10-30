using DtoPaymentStatus = GatewayService.Server.Dto.Models.Payments.Enums.PaymentStatus;
using ApiPaymentStatus = PaymentService.Api.PaymentStatus;

namespace GatewayService.Server.Dto.Converters.Payment.Enums;

public static class PaymentStatusConverter
{
    public static DtoPaymentStatus Convert(ApiPaymentStatus apiPaymentStatus)
    {
        return apiPaymentStatus switch
        {
            ApiPaymentStatus.Paid => DtoPaymentStatus.Paid,
            ApiPaymentStatus.Canceled => DtoPaymentStatus.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(apiPaymentStatus), apiPaymentStatus, null)
        };
    }
}