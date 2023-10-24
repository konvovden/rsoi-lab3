using CarsService.Core.Exceptions;
using CarsService.Core.Models;
using CarsService.Core.Services;
using CarsService.Database.Context;
using CarsService.Services.Converters;
using Microsoft.EntityFrameworkCore;

namespace CarsService.Services;

public class CarsService : ICarsService
{
    private readonly CarsServiceContext _dbContext;

    public CarsService(CarsServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Car> GetCarAsync(Guid id)
    {
        var car = await _dbContext.Cars
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (car is null)
            throw new CarNotFoundException(id);
        
        return CarConverter.Convert(car);
    }

    public Task<int> GetCarsAmountAsync()
    {
        return _dbContext.Cars.CountAsync();
    }

    public async Task<List<Car>> GetCarsAsync(int offset, int limit, bool onlyAvailable)
    {
        var query = _dbContext.Cars
            .AsQueryable();

        if (onlyAvailable)
            query = query.Where(c => c.Availability == true);
                
        var cars = await query
            .OrderBy(c => c.Id)
            .Skip(offset)
            .Take(limit)
            .AsNoTracking()
            .ToListAsync();

        return cars.ConvertAll(CarConverter.Convert);
    }

    public async Task<List<Car>> GetCarsByIdsAsync(List<Guid> ids)
    {
        var cars = await _dbContext.Cars
            .Where(c => ids.Contains(c.Id))
            .AsNoTracking()
            .ToListAsync();

        return cars.ConvertAll(CarConverter.Convert);
    }

    public async Task<Car> ReserveCarAsync(Guid id)
    {
        var car = await _dbContext.Cars
            .FirstOrDefaultAsync(c => c.Id == id);

        if (car is null)
            throw new CarNotFoundException(id);

        if (car.Availability == false)
            throw new CarAlreadyReservedException(id);
        
        car.Availability = false;

        await _dbContext.SaveChangesAsync();

        return CarConverter.Convert(car);
    }

    public async Task<Car> RemoveReserveFromCarAsync(Guid id)
    {
        var car = await _dbContext.Cars
            .FirstOrDefaultAsync(c => c.Id == id);

        if (car is null)
            throw new CarNotFoundException(id);

        car.Availability = true;

        await _dbContext.SaveChangesAsync();

        return CarConverter.Convert(car);
    }
}