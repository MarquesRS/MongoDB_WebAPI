using api.Entities;

namespace api.Repositories;

public interface IBrandRepository
{
    Task<List<BrandEntity>> GetAllAsync();

    Task<BrandEntity> GetByIdAsync(string brandId);

    Task CreateAsync(BrandEntity brand);
    
    Task UpdateAsync(BrandEntity brand);

    Task DeleteAsync(string brandId);
  }
  
  