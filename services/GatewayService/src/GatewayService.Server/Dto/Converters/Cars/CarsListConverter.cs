using CarsService.Api;
using GatewayService.Dto.Cars;

namespace GatewayService.Server.Dto.Converters.Cars;

public static class CarsListConverter
{
    public static CarsList Convert(GetCarsListResponse getCarsListResponse)
    {
        return new CarsList(getCarsListResponse.Page,
            getCarsListResponse.Size,
            getCarsListResponse.TotalAmount,
            getCarsListResponse.Cars.ToList().ConvertAll(CarConverter.Convert)!);
    }
}