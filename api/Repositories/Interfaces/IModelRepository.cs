using api.Entities;
using api.Models;

namespace api.Repositories;

public interface IModelRepository
{
    Task<List<ModelEntity>> GetAllAsync();

    Task<ModelEntity?> GetByIdAsync(string id);

    Task<ModelEntity> CreateAsync(ModelEntity entity);
    
    Task<bool> UpdateAsync(ModelEntity entity);

    Task<bool> DeleteAsync(string id);
  }
  
  