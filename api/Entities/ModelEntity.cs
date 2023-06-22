using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Entities;

public class ModelEntity 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string? Id {get; set;}
    

    [BsonIgnoreIfDefault]
    public string? BrandId {get; set;}


    [BsonIgnoreIfDefault]
    public string? Name {get; set;}
}