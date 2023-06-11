using api.Entities;

namespace api.Repositories;

public interface ICarRepository
{
    Task<List<CarEntity>> GetAllAsync();

    Task<CarEntity> GetByIdAsync(string carId);

    Task CreateAsync(CarEntity car);
    
    Task UpdateAsync(CarEntity car);

    Task DeleteAsync(string carId);
  }
  
  