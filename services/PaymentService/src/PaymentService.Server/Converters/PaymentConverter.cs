using PaymentService.Server.Converters.Enums;
using ApiPayment = PaymentService.Api.Payment;
using CorePayment = PaymentService.Core.Models.Payment;

namespace PaymentService.Server.Converters;

public static class PaymentConverter
{
    public static ApiPayment Convert(CorePayment corePayment)
    {
        return new ApiPayment()
        {
            Id = corePayment.Id.ToString(),
            Price = corePayment.Price,
            Status = PaymentStatusConverter.Convert(corePayment.Status)
        };
    }
}