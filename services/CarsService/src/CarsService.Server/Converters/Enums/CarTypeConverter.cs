using CoreCarType = CarsService.Core.Models.Enums.CarType;
using ApiCarType = CarsService.Api.CarType;

namespace CarsService.Server.Converters.Enums;

public static class CarTypeConverter
{
    public static ApiCarType Convert(CoreCarType coreCarType)
    {
        return coreCarType switch
        {
            CoreCarType.Sedan => ApiCarType.Sedan,
            CoreCarType.Suv => ApiCarType.Suv,
            CoreCarType.Minivan => ApiCarType.Minivan,
            CoreCarType.Roadster => ApiCarType.Roadster,
            _ => throw new ArgumentOutOfRangeException(nameof(coreCarType), coreCarType, null)
        };
    }
}