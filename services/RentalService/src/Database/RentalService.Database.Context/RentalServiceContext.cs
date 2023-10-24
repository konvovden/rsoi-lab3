using Microsoft.EntityFrameworkCore;
using RentalService.Database.Models;

namespace RentalService.Database.Context;

#nullable disable
public class RentalServiceContext : DbContext
{
    public DbSet<Rental> Rentals { get; set; }

    public RentalServiceContext()
    {
        
    }

    public RentalServiceContext(DbContextOptions options) : base(options)
    {
        
    }
}
#nullable restore