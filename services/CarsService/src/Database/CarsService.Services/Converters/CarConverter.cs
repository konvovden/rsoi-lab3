using CarsService.Services.Converters.Enums;
using DbCar = CarsService.Database.Models.Car;
using CoreCar = CarsService.Core.Models.Car;

namespace CarsService.Services.Converters;

public static class CarConverter
{
    public static CoreCar Convert(DbCar dbCar)
    {
        return new CoreCar(dbCar.Id,
            dbCar.Brand,
            dbCar.Model,
            dbCar.RegistrationNumber,
            dbCar.Power,
            dbCar.Price,
            CarTypeConverter.Convert(dbCar.Type),
            dbCar.Availability);
    }
}