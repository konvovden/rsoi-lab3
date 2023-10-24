using RentalService.Core.Models.Enums;

namespace RentalService.Core.Exceptions;

public class RentalNotInProgressException : Exception
{
    public RentalNotInProgressException(Guid id) : base($"Rental {id} not in progress")
    {
        
    }
}