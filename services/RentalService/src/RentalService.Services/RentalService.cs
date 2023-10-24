using Microsoft.EntityFrameworkCore;
using RentalService.Core.Exceptions;
using RentalService.Core.Models;
using RentalService.Core.Models.Enums;
using RentalService.Core.Services;
using RentalService.Database.Context;
using RentalService.Services.Converters;
using RentalService.Services.Converters.Enums;
using DbRental = RentalService.Database.Models.Rental;

namespace RentalService.Services;

public class RentalService : IRentalService
{
    private readonly RentalServiceContext _dbContext;

    public RentalService(RentalServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Rental> GetUserRentalAsync(Guid rentalId, string username)
    {
        var rental = await _dbContext.Rentals
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == rentalId && r.Username == username);

        if (rental is null)
            throw new RentalNotFoundException(rentalId);

        if (rental.Username != username)
            throw new ForbiddenException($"User {username} is not owner of Rental {rentalId}");
        
        return RentalConverter.Convert(rental);
    }

    public async Task<List<Rental>> GetUserRentalsAsync(string username)
    {
        var rentals = await _dbContext.Rentals
            .Where(r => r.Username == username)
            .AsNoTracking()
            .ToListAsync();

        return rentals.ConvertAll(RentalConverter.Convert);
    }

    public async Task<Rental> CreateRentalAsync(Guid id, 
        string username,
        Guid paymentId,
        Guid carId,
        DateOnly dateFrom,
        DateOnly dateTo,
        RentalStatus status)
    {
        var dbRental = new DbRental(id,
            username,
            paymentId,
            carId,
            dateFrom,
            dateTo,
            RentalStatusConverter.Convert(status));

        await _dbContext.Rentals.AddAsync(dbRental);
        await _dbContext.SaveChangesAsync();

        return RentalConverter.Convert(dbRental);
    }

    public async Task<Rental> FinishRentalForUserAsync(Guid rentalId, string username)
    {
        var rental = await _dbContext.Rentals
            .FirstOrDefaultAsync(r => r.Id == rentalId);

        if (rental is null)
            throw new RentalNotFoundException(rentalId);
        
        if (rental.Username != username)
            throw new ForbiddenException($"User {username} is not owner of Rental {rentalId}");
        
        if (rental.Status != Database.Models.Enums.RentalStatus.InProgress)
            throw new RentalNotInProgressException(rentalId);
        
        rental.Status = Database.Models.Enums.RentalStatus.Finished;

        await _dbContext.SaveChangesAsync();

        return RentalConverter.Convert(rental);
    }

    public async Task<Rental> CancelRentalForUserAsync(Guid rentalId, string username)
    {
        var rental = await _dbContext.Rentals
            .FirstOrDefaultAsync(r => r.Id == rentalId);

        if (rental is null)
            throw new RentalNotFoundException(rentalId);

        if (rental.Username != username)
            throw new ForbiddenException($"User {username} is not owner of Rental {rentalId}");
        
        if (rental.Status != Database.Models.Enums.RentalStatus.InProgress)
            throw new RentalNotInProgressException(rentalId);

        rental.Status = Database.Models.Enums.RentalStatus.Canceled;

        await _dbContext.SaveChangesAsync();

        return RentalConverter.Convert(rental);
    }
}