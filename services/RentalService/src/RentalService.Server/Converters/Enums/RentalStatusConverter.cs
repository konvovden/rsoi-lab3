using ApiRentalStatus = RentalService.Api.RentalStatus;
using CoreRentalStatus = RentalService.Core.Models.Enums.RentalStatus;

namespace RentalService.Server.Converters.Enums;

public static class RentalStatusConverter
{
    public static ApiRentalStatus Convert(CoreRentalStatus coreRentalStatus)
    {
        return coreRentalStatus switch
        {
            CoreRentalStatus.New => ApiRentalStatus.New,
            CoreRentalStatus.InProgress => ApiRentalStatus.InProgress,
            CoreRentalStatus.Finished => ApiRentalStatus.Finished,
            CoreRentalStatus.Canceled => ApiRentalStatus.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(coreRentalStatus), coreRentalStatus, null)
        };
    }
}