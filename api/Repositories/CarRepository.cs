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
        return await _context.CarsCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<CarEntity> GetByIdAsync(string carId)
    {
        FilterDefinition<CarEntity> filter = Builders<CarEntity>.Filter.Eq("Id", carId);
        return await _context.CarsCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(CarEntity car) 
    {
        await _context.CarsCollection.InsertOneAsync(car);
    }

    public async Task UpdateAsync(CarEntity car)
    {
        FilterDefinition<CarEntity> filter = Builders<CarEntity>.Filter.Eq("Id", car.Id);
        await _context.CarsCollection.ReplaceOneAsync(filter, car);
    }

    public async Task DeleteAsync(string carId)
    {
        FilterDefinition<CarEntity> filter = Builders<CarEntity>.Filter.Eq("Id", carId);
        await _context.CarsCollection.DeleteOneAsync(filter);
    }
}
  
  