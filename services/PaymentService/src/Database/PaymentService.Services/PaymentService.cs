using Microsoft.EntityFrameworkCore;
using PaymentService.Core.Exceptions;
using PaymentService.Core.Models;
using PaymentService.Core.Services;
using PaymentService.Database.Context;
using PaymentService.Services.Converters;
using DbPayment = PaymentService.Database.Models.Payment;
using PaymentStatus = PaymentService.Database.Models.Enums.PaymentStatus;

namespace PaymentService.Services;

public class PaymentService : IPaymentService
{
    private readonly PaymentServiceContext _dbContext;

    public PaymentService(PaymentServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Payment> GetPaymentAsync(Guid id)
    {
        var payment = await _dbContext.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (payment is null)
            throw new PaymentNotFoundException(id);
        
        return PaymentConverter.Convert(payment);
    }

    public async Task<List<Payment>> GetPaymentsAsync(List<Guid> ids)
    {
        var payments = await _dbContext.Payments
            .Where(p => ids.Contains(p.Id))
            .AsNoTracking()
            .ToListAsync();

        return payments.ConvertAll(PaymentConverter.Convert);
    }

    public async Task<Payment> CreatePaymentAsync(Guid id, int price)
    {
        var payment = new DbPayment(id, PaymentStatus.Paid, price);

        await _dbContext.Payments.AddAsync(payment);
        await _dbContext.SaveChangesAsync();

        return PaymentConverter.Convert(payment);
    }

    public async Task<Payment> CancelPaymentAsync(Guid id)
    {
        var payment = await _dbContext.Payments
            .FirstOrDefaultAsync(p => p.Id == id);

        if (payment is null)
            throw new PaymentNotFoundException(id);
        
        if (payment.Status != PaymentStatus.Paid)
            throw new PaymentNotPaidException(id);
        
        payment.Status = PaymentStatus.Canceled;
        
        await _dbContext.SaveChangesAsync();

        return PaymentConverter.Convert(payment);
    }
}