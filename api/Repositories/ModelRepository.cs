using api.Services;
using api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Repositories;

public class ModelRepository : IModelRepository
{
    private readonly DatabaseContext _context;

    public ModelRepository(DatabaseContext context) 
    {
        _context = context;
    }

    public async Task<List<ModelEntity>> GetAllAsync() 
    {
        var entities = await _context.ModelsCollection
            .FindAsync(new BsonDocument());
        
        return await entities.ToListAsync();   
    }

    public async Task<ModelEntity?> GetByIdAsync(string id)
    {
        var entity = await _context.ModelsCollection
            .FindAsync(Builders<ModelEntity>.Filter.Eq("Id", id));

        return await entity.FirstOrDefaultAsync();
    }

    public async Task<ModelEntity> CreateAsync(ModelEntity entity) 
    {
        await _context.ModelsCollection.InsertOneAsync(entity);
        
        return entity;
    }

    public async Task<bool> UpdateAsync(ModelEntity entity)
    {
        FilterDefinition<ModelEntity> filter = Builders<ModelEntity>.Filter.Eq("Id", entity.Id);
        
        var result = await _context.ModelsCollection.ReplaceOneAsync(filter, entity);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        FilterDefinition<ModelEntity> filter = Builders<ModelEntity>.Filter.Eq("Id", id);
        
        var result = await _context.ModelsCollection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;

    }
}
  
  