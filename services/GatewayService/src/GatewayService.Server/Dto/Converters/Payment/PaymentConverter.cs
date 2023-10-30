using System.Diagnostics.CodeAnalysis;
using GatewayService.Server.Dto.Converters.Payment.Enums;
using ApiPayment = PaymentService.Api.Payment;
using DtoPayment = GatewayService.Dto.Payments.Payment;

namespace GatewayService.Server.Dto.Converters.Payment;

public static class PaymentConverter
{
    
    [return: NotNullIfNotNull("apiPayment")]
    public static DtoPayment? Convert(ApiPayment? apiPayment)
    {
        if (apiPayment is null)
            return null;
        
        return new DtoPayment(apiPayment.Id,
            PaymentStatusConverter.Convert(apiPayment.Status),
            apiPayment.Price);
    }
}