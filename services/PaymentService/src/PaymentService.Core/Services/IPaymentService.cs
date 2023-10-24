using PaymentService.Core.Models;
using PaymentService.Core.Models.Enums;

namespace PaymentService.Core.Services;

public interface IPaymentService
{
    Task<Payment> GetPaymentAsync(Guid id);
    Task<List<Payment>> GetPaymentsAsync(List<Guid> ids);

    Task<Payment> CreatePaymentAsync(Guid id,
        int price);

    Task<Payment> CancelPaymentAsync(Guid id);
}