using CarsService.Core.Models;

namespace CarsService.Core.Services;

public interface ICarsService
{
    Task<Car> GetCarAsync(Guid id);
    Task<int> GetCarsAmountAsync();
    Task<List<Car>> GetCarsAsync(int offset, int limit, bool onlyAvailable);
    Task<List<Car>> GetCarsByIdsAsync(List<Guid> ids);
    Task<Car> ReserveCarAsync(Guid id);
    Task<Car> RemoveReserveFromCarAsync(Guid id);
}