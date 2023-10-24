using RentalService.Services.Converters.Enums;
using DbRental = RentalService.Database.Models.Rental;
using CoreRental = RentalService.Core.Models.Rental;

namespace RentalService.Services.Converters;

public static class RentalConverter
{
    public static CoreRental Convert(DbRental dbRental)
    {
        return new CoreRental(dbRental.Id,
            dbRental.Username,
            dbRental.PaymentId,
            dbRental.CarId,
            dbRental.DateFrom,
            dbRental.DateTo,
            RentalStatusConverter.Convert(dbRental.Status));
    }
}