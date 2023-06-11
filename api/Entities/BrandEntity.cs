using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Entities;

public class BrandEntity 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}

    public string Name {get; set;} = string.Empty;
}