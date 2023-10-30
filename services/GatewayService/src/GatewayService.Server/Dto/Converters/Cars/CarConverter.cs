using System.Diagnostics.CodeAnalysis;
using GatewayService.Server.Dto.Converters.Cars.Enums;
using DtoCar = GatewayService.Server.Dto.Models.Cars.Car;
using ApiCar = CarsService.Api.Car;

namespace GatewayService.Server.Dto.Converters.Cars;

public static class CarConverter
{
    [return: NotNullIfNotNull("apiCar")]
    public static DtoCar? Convert(ApiCar? apiCar)
    {
        if (apiCar is null)
            return null;
        
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