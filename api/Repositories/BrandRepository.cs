using api.Services;
using api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using api.Models;

namespace api.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly DatabaseContext _context;

    public BrandRepository(DatabaseContext context) 
    {
        _context = context;
    }

    public async Task<List<BrandEntity>> GetAllAsync() 
    {
        var brandsEntities = await _context.BrandsCollection
            .FindAsync(new BsonDocument());
        
        return await brandsEntities.ToListAsync();   
    }

    public async Task<BrandEntity?> GetByIdAsync(string brandId)
    {
        var brandEntity = await _context.BrandsCollection
            .FindAsync(Builders<BrandEntity>.Filter.Eq("Id", brandId));

        return await brandEntity.FirstOrDefaultAsync();
    }

    public async Task<BrandEntity> CreateAsync(BrandEntity brand) 
    {
        await _context.BrandsCollection.InsertOneAsync(brand);
        
        return brand;
    }

    public async Task<bool> UpdateAsync(BrandEntity brand)
    {
        FilterDefinition<BrandEntity> filter = Builders<BrandEntity>.Filter.Eq("Id", brand.Id);
        
        var result = await _context.BrandsCollection.ReplaceOneAsync(filter, brand);
    
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string brandId)
    {
        FilterDefinition<BrandEntity> filter = Builders<BrandEntity>.Filter.Eq("Id", brandId);
        
        var result = await _context.BrandsCollection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;

    }
}
  
  