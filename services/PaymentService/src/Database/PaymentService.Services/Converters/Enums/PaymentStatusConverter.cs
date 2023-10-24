using DbPaymentStatus = PaymentService.Database.Models.Enums.PaymentStatus;
using CorePaymentStatus = PaymentService.Core.Models.Enums.PaymentStatus;

namespace PaymentService.Services.Converters.Enums;

public static class PaymentStatusConverter
{
    public static CorePaymentStatus Convert(DbPaymentStatus dbPaymentStatus)
    {
        return dbPaymentStatus switch
        {
            DbPaymentStatus.Paid => CorePaymentStatus.Paid,
            DbPaymentStatus.Canceled => CorePaymentStatus.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(dbPaymentStatus), dbPaymentStatus, null)
        };
    }

    public static DbPaymentStatus Convert(CorePaymentStatus corePaymentStatus)
    {
        return corePaymentStatus switch
        {
            CorePaymentStatus.Paid => DbPaymentStatus.Paid,
            CorePaymentStatus.Canceled => DbPaymentStatus.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(corePaymentStatus), corePaymentStatus, null)
        };
    }
}