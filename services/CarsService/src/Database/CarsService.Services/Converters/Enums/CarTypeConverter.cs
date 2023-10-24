using CarsService.Core.Models.Enums;
using CoreCarType = CarsService.Core.Models.Enums.CarType;
using DbCarType = CarsService.Database.Models.Enums.CarType;

namespace CarsService.Services.Converters.Enums;

public static class CarTypeConverter
{
    public static CoreCarType Convert(DbCarType dbCarType)
    {
        return dbCarType switch
        {
            DbCarType.Sedan => CoreCarType.Sedan,
            DbCarType.Suv => CoreCarType.Suv,
            DbCarType.Minivan => CoreCarType.Minivan,
            DbCarType.Roadster => CoreCarType.Roadster,
            _ => throw new ArgumentOutOfRangeException(nameof(dbCarType), dbCarType, null)
        };
    }
}