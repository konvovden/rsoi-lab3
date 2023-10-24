using RentalService.Core.Models;
using RentalService.Core.Models.Enums;

namespace RentalService.Core.Services;

public interface IRentalService
{
    Task<Rental> GetUserRentalAsync(Guid rentalId, string username);
    Task<List<Rental>> GetUserRentalsAsync(string username);

    Task<Rental> CreateRentalAsync(Guid id,
        string username,
        Guid paymentId,
        Guid carId,
        DateOnly dateFrom,
        DateOnly dateTo,
        RentalStatus status);

    Task<Rental> FinishRentalForUserAsync(Guid rentalId, string username);
    Task<Rental> CancelRentalForUserAsync(Guid rentalId, string username);
}