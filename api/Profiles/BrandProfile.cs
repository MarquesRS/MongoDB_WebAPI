using api.Entities;
using AutoMapper;
using MongoDB.Bson;

namespace api.Profiles;

public class BrandProfile : Profile {
    public BrandProfile() {
        // Origem, Destino
        CreateMap<BrandEntity, BsonDocument>();
        
    }
}