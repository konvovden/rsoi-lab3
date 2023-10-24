using CarsService.Server.Converters.Enums;
using CoreCar = CarsService.Core.Models.Car;
using ApiCar = CarsService.Api.Car;

namespace CarsService.Server.Converters;

public static class CarConverter
{
    public static ApiCar Convert(CoreCar coreCar)
    {
        return new ApiCar()
        {
            Id = coreCar.Id.ToString(),
            Brand = coreCar.Brand,
            Model = coreCar.Model,
            RegistrationNumber = coreCar.RegistrationNumber,
            Power = coreCar.Power,
            Price = coreCar.Price,
            Type = CarTypeConverter.Convert(coreCar.Type),
            Availability = coreCar.Availability
        };
    }
}