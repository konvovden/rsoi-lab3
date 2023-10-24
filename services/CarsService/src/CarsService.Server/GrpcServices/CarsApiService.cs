using CarsService.Api;
using CarsService.Core.Services;
using CarsService.Server.Converters;
using Grpc.Core;

namespace CarsService.Server.GrpcServices;

public class CarsApiService : Api.CarsService.CarsServiceBase
{
    private readonly ICarsService _carsService;

    public CarsApiService(ICarsService carsService)
    {
        _carsService = carsService;
    }

    public override async Task<GetCarsListResponse> GetCarsList(GetCarsListRequest request, ServerCallContext context)
    {
        var totalAmount = await _carsService.GetCarsAmountAsync();

        var offset = (request.Page - 1) * request.Size;

        var cars = await _carsService.GetCarsAsync(offset, request.Size, !request.ShowAll);

        return new GetCarsListResponse()
        {
            Page = request.Page,
            Size = request.Size,
            TotalAmount = totalAmount,
            Cars = { cars.ConvertAll(CarConverter.Convert) }
        };
    }

    public override async Task<GetCarsResponse> GetCars(GetCarsRequest request, ServerCallContext context)
    {
        var carIds = request.Ids
            .Select(Guid.Parse)
            .ToList();

        var cars = await _carsService.GetCarsByIdsAsync(carIds);

        var notFoundIds = carIds
            .Except(cars.Select(c => c.Id))
            .Select(id => id.ToString());

        return new GetCarsResponse()
        {
            Cars = {cars.ConvertAll(CarConverter.Convert)},
            NotFoundIds = {notFoundIds}
        };
    }

    public override async Task<GetCarResponse> GetCar(GetCarRequest request, ServerCallContext context)
    {
        var carId = Guid.Parse(request.Id);

        var car = await _carsService.GetCarAsync(carId);

        return new GetCarResponse()
        {
            Car = CarConverter.Convert(car)
        };
    }

    public override async Task<ReserveCarResponse> ReserveCar(ReserveCarRequest request, ServerCallContext context)
    {
        var carId = Guid.Parse(request.Id);

        var car = await _carsService.ReserveCarAsync(carId);

        return new ReserveCarResponse()
        {
            Car = CarConverter.Convert(car)
        };
    }

    public override async Task<RemoveReserveFromCarResponse> RemoveReserveFromCar(RemoveReserveFromCarRequest request, ServerCallContext context)
    {
        var carId = Guid.Parse(request.Id);

        var car = await _carsService.RemoveReserveFromCarAsync(carId);

        return new RemoveReserveFromCarResponse()
        {
            Car = CarConverter.Convert(car)
        };
    }
}