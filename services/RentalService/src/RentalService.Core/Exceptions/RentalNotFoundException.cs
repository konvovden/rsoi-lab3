namespace RentalService.Core.Exceptions;

public class RentalNotFoundException : Exception
{
    public RentalNotFoundException(Guid id) : base($"Rental {id} not found")
    {
        
    }
}