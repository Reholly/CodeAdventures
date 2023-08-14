using Data.Entities;
using Data.Repositories;

namespace Server.Services.UserServices;

public class UserService : IUserService
{
    private readonly IRepository<User> _usersDb;

    public UserService(IRepository<User> usersDb)
    {
        _usersDb = usersDb;
    }
    public async Task<User> CreateUserAsync(User freshUser)
    {
         var newUser = new User
        {
            Name = freshUser.Name,
            Username = freshUser.Username,
            Email = freshUser.Email,
            Surname = freshUser.Surname,
            Patronymic = freshUser.Patronymic,
            RegistrationDate = DateTime.UtcNow,
            Role = "Student"
        };
         
         await _usersDb.AddAsync(newUser);
        
        return newUser;
    }

    public async Task<User?> FindByEmail(string email)
    {
        var users = await _usersDb.GetTableAsync();
        return users.FirstOrDefault(u => u.Email == email);
    }
}