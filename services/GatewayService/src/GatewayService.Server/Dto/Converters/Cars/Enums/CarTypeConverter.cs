using DtoCarType = GatewayService.Dto.Cars.Enums.CarType;
using ApiCarType = CarsService.Api.CarType;

namespace GatewayService.Server.Dto.Converters.Cars.Enums;

public static class CarTypeConverter
{
    public static DtoCarType Convert(ApiCarType apiCarType)
    {
        return apiCarType switch
        {
            ApiCarType.Sedan => DtoCarType.Sedan,
            ApiCarType.Suv => DtoCarType.Suv,
            ApiCarType.Minivan => DtoCarType.Minivan,
            ApiCarType.Roadster => DtoCarType.Roadster,
            _ => throw new ArgumentOutOfRangeException(nameof(apiCarType), apiCarType, null)
        };
    }
}