using RentalService.Database.Models.Enums;

namespace RentalService.Database.Models;

public class Rental
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public Guid PaymentId { get; set; }
    public Guid CarId { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public RentalStatus Status { get; set; }

    public Rental(Guid id, 
        string username,
        Guid paymentId,
        Guid carId,
        DateOnly dateFrom,
        DateOnly dateTo,
        RentalStatus status)
    {
        Id = id;
        Username = username;
        PaymentId = paymentId;
        CarId = carId;
        DateFrom = dateFrom;
        DateTo = dateTo;
        Status = status;
    }
}