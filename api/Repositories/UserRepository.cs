using api.Entities;
using api.Services;

namespace api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context) 
    {
        _context = context;
    }

    public UserEntity? Get(string username, string password)
    {
        var users = new List<UserEntity>();

        users.Add(new UserEntity {Id = 1, Username = "Pablo", Password = "123"});

        return users
            .Where(u => 
                u.Username.ToUpper() == username.ToUpper()
                && u.Password == password
            ).FirstOrDefault();
    }
}