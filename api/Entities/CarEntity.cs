using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Entities;

public class CarEntity 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}

    public string? ModelId {get; set;}

    public string Name {get; set;} = string.Empty;

    public int Renavam {get; set;}

    public string Plate {get; set;} = string.Empty;

    public decimal value {get; set;}

    public int Year {get; set;}
}