using ApiPaymentStatus = PaymentService.Api.PaymentStatus;
using CorePaymentStatus = PaymentService.Core.Models.Enums.PaymentStatus;

namespace PaymentService.Server.Converters.Enums;

public static class PaymentStatusConverter
{
    public static ApiPaymentStatus Convert(CorePaymentStatus corePaymentStatus)
    {
        return corePaymentStatus switch
        {
            CorePaymentStatus.Paid => ApiPaymentStatus.Paid,
            CorePaymentStatus.Canceled => ApiPaymentStatus.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(corePaymentStatus), corePaymentStatus, null)
        };
    }
}