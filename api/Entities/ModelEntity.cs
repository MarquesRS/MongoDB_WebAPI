using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Entities;

public class ModelEntity 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}

    public string? BrandId {get; set;}

    public string Name {get; set;} = string.Empty;
}