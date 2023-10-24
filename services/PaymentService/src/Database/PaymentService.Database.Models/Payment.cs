using PaymentService.Database.Models.Enums;

namespace PaymentService.Database.Models;

public class Payment
{
    public Guid Id { get; set; }
    public PaymentStatus Status { get; set; }
    public int Price { get; set; }

    public Payment(Guid id, 
        PaymentStatus status,
        int price)
    {
        Id = id;
        Status = status;
        Price = price;
    }
}