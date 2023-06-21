using api.Entities;
using api.Models;

namespace api.Repositories;

public interface ICarRepository
{
    Task<List<CarEntity>> GetAllAsync();

    Task<CarEntity?> GetByIdAsync(string id);

    Task<CarEntity> CreateAsync(CarEntity entity);
    
    Task<bool> UpdateAsync(CarEntity entity);

    Task<bool> DeleteAsync(string id);
  }
  
  