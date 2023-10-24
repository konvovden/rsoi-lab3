using DbRentalStatus = RentalService.Database.Models.Enums.RentalStatus;
using CoreRentalStatus = RentalService.Core.Models.Enums.RentalStatus;

namespace RentalService.Services.Converters.Enums;

public static class RentalStatusConverter
{
    public static CoreRentalStatus Convert(DbRentalStatus dbRentalStatus)
    {
        return dbRentalStatus switch
        {
            DbRentalStatus.New => CoreRentalStatus.New,
            DbRentalStatus.InProgress => CoreRentalStatus.InProgress,
            DbRentalStatus.Finished => CoreRentalStatus.Finished,
            DbRentalStatus.Canceled => CoreRentalStatus.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(dbRentalStatus), dbRentalStatus, null)
        };
    }
    
    public static DbRentalStatus Convert(CoreRentalStatus coreRentalStatus)
    {
        return coreRentalStatus switch
        {
            CoreRentalStatus.New => DbRentalStatus.New,
            CoreRentalStatus.InProgress => DbRentalStatus.InProgress,
            CoreRentalStatus.Finished => DbRentalStatus.Finished,
            CoreRentalStatus.Canceled => DbRentalStatus.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(coreRentalStatus), coreRentalStatus, null)
        };
    }
}