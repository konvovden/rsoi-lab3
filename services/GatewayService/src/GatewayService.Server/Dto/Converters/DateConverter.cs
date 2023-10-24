using RentalService.Api;

namespace GatewayService.Server.Dto.Converters;

public static class DateConverter
{
    public static Date Convert(DateOnly dateOnly)
    {
        return new Date()
        {
            Day = dateOnly.Day,
            Month = dateOnly.Month,
            Year = dateOnly.Year
        };
    }

    public static DateOnly Convert(Date date)
    {
        return new DateOnly(date.Year, date.Month, date.Day);
    }
}