using PaymentService.Services.Converters.Enums;
using CorePayment = PaymentService.Core.Models.Payment;
using DbPayment = PaymentService.Database.Models.Payment;

namespace PaymentService.Services.Converters;

public static class PaymentConverter
{
    public static CorePayment Convert(DbPayment dbPayment)
    {
        return new CorePayment(dbPayment.Id,
            PaymentStatusConverter.Convert(dbPayment.Status),
            dbPayment.Price);
    }
}