using RentalService.Server.Converters.Enums;
using ApiRental = RentalService.Api.Rental;
using CoreRental = RentalService.Core.Models.Rental;

namespace RentalService.Server.Converters;

public static class RentalConverter
{
    public static ApiRental Convert(CoreRental coreRental)
    {
        return new ApiRental()
        {
            Id = coreRental.Id.ToString(),
            Username = coreRental.Username,
            PaymentId = coreRental.PaymentId.ToString(),
            CarId = coreRental.CarId.ToString(),
            DateFrom = DateConverter.Convert(coreRental.DateFrom),
            DateTo = DateConverter.Convert(coreRental.DateTo),
            Status = RentalStatusConverter.Convert(coreRental.Status)
        };
    }
}