using api.Entities;

namespace api.Repositories;

public interface IUserRepository
{
    public UserEntity? Get(string username, string password);
}

