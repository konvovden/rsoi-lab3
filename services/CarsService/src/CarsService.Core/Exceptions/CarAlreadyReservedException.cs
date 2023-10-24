namespace CarsService.Core.Exceptions;

public class CarAlreadyReservedException : Exception
{
    public CarAlreadyReservedException(Guid id) : base($"Car {id} already reserved")
    {
        
    }
}