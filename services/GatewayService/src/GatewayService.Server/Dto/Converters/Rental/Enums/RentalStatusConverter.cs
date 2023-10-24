using ApiRentalStatus = RentalService.Api.RentalStatus;
using DtoRentalStatus = GatewayService.Dto.Rental.Enums.RentalStatus;

namespace GatewayService.Server.Dto.Converters.Rental.Enums;

public static class RentalStatusConverter
{
    public static DtoRentalStatus Convert(ApiRentalStatus apiRentalStatus)
    {
        return apiRentalStatus switch
        {
            ApiRentalStatus.New => DtoRentalStatus.New,
            ApiRentalStatus.InProgress => DtoRentalStatus.InProgress,
            ApiRentalStatus.Finished => DtoRentalStatus.Finished,
            ApiRentalStatus.Canceled => DtoRentalStatus.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(apiRentalStatus), apiRentalStatus, null)
        };
    }
}