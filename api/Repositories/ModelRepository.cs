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
        return await _context.ModelsCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<ModelEntity> GetByIdAsync(string modelId)
    {
        FilterDefinition<ModelEntity> filter = Builders<ModelEntity>.Filter.Eq("Id", modelId);
        return await _context.ModelsCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(ModelEntity model) 
    {
        await _context.ModelsCollection.InsertOneAsync(model);
    }

    public async Task UpdateAsync(ModelEntity model)
    {
        FilterDefinition<ModelEntity> filter = Builders<ModelEntity>.Filter.Eq("Id", model.Id);
        await _context.ModelsCollection.ReplaceOneAsync(filter, model);
    }

    public async Task DeleteAsync(string modelId)
    {
        FilterDefinition<ModelEntity> filter = Builders<ModelEntity>.Filter.Eq("Id", modelId);
        await _context.ModelsCollection.DeleteOneAsync(filter);
    }
}
  
  