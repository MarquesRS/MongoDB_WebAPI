using api.Entities;
using api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api.Services;

public class DatabaseContext
{
    public IMongoCollection<BrandEntity> BrandsCollection {get; set;}

    public IMongoCollection<ModelEntity> ModelsCollection {get; set;}

    public IMongoCollection<CarEntity> CarsCollection {get; set;}

    public DatabaseContext(IOptions<DatabaseModel> mongoDbModel) 
    {
        MongoClient client = new MongoClient(mongoDbModel.Value.ConnectionURI);

        IMongoDatabase database = client.GetDatabase(mongoDbModel.Value.DatabaseName);

        BrandsCollection = database.GetCollection<BrandEntity>("brands");

        ModelsCollection = database.GetCollection<ModelEntity>("models");
        
        CarsCollection = database.GetCollection<CarEntity>("cars");
    }
}