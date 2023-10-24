using GatewayService.Server.Dto.Converters.Cars.Enums;
using DtoCar = GatewayService.Server.Dto.Models.Cars.Car;
using ApiCar = CarsService.Api.Car;

namespace GatewayService.Server.Dto.Converters.Cars;

public static class CarConverter
{
    public static DtoCar Convert(ApiCar apiCar)
    {
        return new DtoCar(apiCar.Id,
            apiCar.Brand,
            apiCar.Model,
            apiCar.RegistrationNumber,
            apiCar.Power,
            CarTypeConverter.Convert(apiCar.Type),
            apiCar.Price,
            apiCar.Availability);
    }
}