using Grpc.Core;
using RentalService.Api;
using RentalService.Core.Services;
using RentalService.Server.Converters;
using RentalStatus = RentalService.Core.Models.Enums.RentalStatus;

namespace RentalService.Server.GrpcServices;

public class RentalApiService : Api.RentalService.RentalServiceBase
{
    private readonly IRentalService _rentalService;

    public RentalApiService(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public override async Task<GetUserRentalResponse> GetUserRental(GetUserRentalRequest request, ServerCallContext context)
    {
        var rentalId = Guid.Parse(request.RentalId);

        var rental = await _rentalService.GetUserRentalAsync(rentalId, request.Username);

        return new GetUserRentalResponse()
        {
            Rental = RentalConverter.Convert(rental)
        };
    }

    public override async Task<GetUserRentalsResponse> GetUserRentals(GetUserRentalsRequest request, ServerCallContext context)
    {
        var rentals = await _rentalService.GetUserRentalsAsync(request.Username);

        return new GetUserRentalsResponse()
        {
            Rentals = { rentals.ConvertAll(RentalConverter.Convert) }
        };
    }

    public override async Task<CreateRentalResponse> CreateRental(CreateRentalRequest request, ServerCallContext context)
    {
        var rental = await _rentalService.CreateRentalAsync(Guid.NewGuid(),
            request.Username,
            Guid.Parse(request.PaymentId),
            Guid.Parse(request.CarId),
            DateConverter.Convert(request.DateFrom),
            DateConverter.Convert(request.DateTo),
            RentalStatus.InProgress);

        return new CreateRentalResponse()
        {
            Rental = RentalConverter.Convert(rental)
        };
    }

    public override async Task<FinishRentalResponse> FinishRental(FinishRentalRequest request, ServerCallContext context)
    {
        var rental = await _rentalService.FinishRentalForUserAsync(Guid.Parse(request.RentalId), request.Username);

        return new FinishRentalResponse()
        {
            Rental = RentalConverter.Convert(rental)
        };
    }

    public override async Task<CancelRentalResponse> CancelRental(CancelRentalRequest request, ServerCallContext context)
    {
        var rental = await _rentalService.CancelRentalForUserAsync(Guid.Parse(request.RentalId), request.Username);

        return new CancelRentalResponse()
        {
            Rental = RentalConverter.Convert(rental)
        };
    }
}