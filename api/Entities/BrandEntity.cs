using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Entities
{
    public class BrandEntity 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id {get; set;}
        

        [BsonIgnoreIfDefault]
        public string? Name { get; set; }
    }
}