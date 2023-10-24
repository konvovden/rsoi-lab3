using Microsoft.EntityFrameworkCore;
using PaymentService.Database.Models;

namespace PaymentService.Database.Context;

#nullable disable
public class PaymentServiceContext : DbContext
{
    public DbSet<Payment> Payments { get; set; }

    public PaymentServiceContext()
    {
        
    }

    public PaymentServiceContext(DbContextOptions options) : base(options)
    {
        
    }
}
#nullable restore