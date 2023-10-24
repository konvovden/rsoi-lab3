using CarsService.Database.Models;
using CarsService.Database.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarsService.Database.Context;

#nullable disable
public class CarsServiceContext : DbContext
{
    public DbSet<Car> Cars { get; set; }

    public CarsServiceContext()
    {
        
    }

    public CarsServiceContext(DbContextOptions options) : base(options)  
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasData(new List<Car>()
            {
                new(Guid.Parse("109b42f3-198d-4c89-9276-a7520a7120ab"),
                    "Mercedes Benz",
                    "GLA 250",
                    "ЛО777Х799",
                    249,
                    3500,
                    CarType.Sedan,
                    true)
            });
    }
}
#nullable restore