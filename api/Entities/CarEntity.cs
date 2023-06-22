using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Entities;

public class CarEntity 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string? Id {get; set;}


    [BsonIgnoreIfDefault]
    public string? ModelId {get; set;}


    [BsonIgnoreIfDefault]
    public string? Name {get; set;}


    [BsonIgnoreIfDefault]
    public int? Renavam {get; set;}


    [BsonIgnoreIfDefault]
    public string? Plate {get; set;}


    [BsonIgnoreIfDefault]
    public decimal? value {get; set;}
    

    [BsonIgnoreIfDefault]
    public int? Year {get; set;}
}