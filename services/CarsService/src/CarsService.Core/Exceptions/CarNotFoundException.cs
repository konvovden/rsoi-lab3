namespace CarsService.Core.Exceptions;

public class CarNotFoundException : Exception
{
    public CarNotFoundException(Guid id) : base($"Car {id} not found") 
    {
        
    }
}