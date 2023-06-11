using api.Services;
using api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

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
        return await _context.BrandsCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<BrandEntity> GetByIdAsync(string brandId)
    {
        FilterDefinition<BrandEntity> filter = Builders<BrandEntity>.Filter.Eq("Id", brandId);
        return await _context.BrandsCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(BrandEntity brand) 
    {
        await _context.BrandsCollection.InsertOneAsync(brand);
    }

    public async Task UpdateAsync(BrandEntity brand)
    {
        FilterDefinition<BrandEntity> filter = Builders<BrandEntity>.Filter.Eq("Id", brand.Id);
        await _context.BrandsCollection.ReplaceOneAsync(filter, brand);
    }

    public async Task DeleteAsync(string brandId)
    {
        FilterDefinition<BrandEntity> filter = Builders<BrandEntity>.Filter.Eq("Id", brandId);
        await _context.BrandsCollection.DeleteOneAsync(filter);
    }
}
  
  