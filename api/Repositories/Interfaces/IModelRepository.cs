using api.Entities;

namespace api.Repositories;

public interface IModelRepository
{
    Task<List<ModelEntity>> GetAllAsync();

    Task<ModelEntity> GetByIdAsync(string modelId);

    Task CreateAsync(ModelEntity model);
    
    Task UpdateAsync(ModelEntity model);

    Task DeleteAsync(string modelId);
  }
  
  