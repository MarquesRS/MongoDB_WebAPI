using api.Entities;
using api.Models;

namespace api.Repositories;

public interface IBrandRepository
{
    Task<List<BrandEntity>> GetAllAsync();

    Task<BrandEntity?> GetByIdAsync(string brandId);

    Task<BrandEntity> CreateAsync(BrandEntity brand);
    
    Task<bool> UpdateAsync(BrandEntity brand);

    Task<bool> DeleteAsync(string brandId);
  }
  
  