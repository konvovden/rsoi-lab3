namespace PaymentService.Core.Exceptions;

public class PaymentNotPaidException : Exception
{
    public PaymentNotPaidException(Guid id) : base($"Payment {id} not paid")
    {
        
    }
}