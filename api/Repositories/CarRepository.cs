using api.Services;
using api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositories;

public class CarRepository : ICarRepository
{
    private readonly DatabaseContext _context;

    public CarRepository(DatabaseContext context) 
    {
        _context = context;
    }

    public async Task<List<CarEntity>> GetAllAsync() 
    {
        var entities = await _context.CarsCollection
            .FindAsync(new BsonDocument());
        
        return await entities.ToListAsync();   
    }

    public async Task<CarEntity?> GetByIdAsync(string id)
    {
        var entity = await _context.CarsCollection
            .FindAsync(Builders<CarEntity>.Filter.Eq("Id", id));

        return await entity.FirstOrDefaultAsync();
    }

    public async Task<CarEntity> CreateAsync(CarEntity entity) 
    {
        await _context.CarsCollection.InsertOneAsync(entity);
        
        return entity;
    }

    public async Task<bool> UpdateAsync(CarEntity entity)
    {
        FilterDefinition<CarEntity> filter = Builders<CarEntity>.Filter.Eq("Id", entity.Id);
        
        var result = await _context.CarsCollection.ReplaceOneAsync(filter, entity);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        FilterDefinition<CarEntity> filter = Builders<CarEntity>.Filter.Eq("Id", id);
        
        var result = await _context.CarsCollection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;

    }
}
  
  