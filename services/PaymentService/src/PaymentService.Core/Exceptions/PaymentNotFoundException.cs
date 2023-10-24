namespace PaymentService.Core.Exceptions;

public class PaymentNotFoundException : Exception
{
    public PaymentNotFoundException(Guid id) : base($"Payment {id} not found")
    {
        
    }
}