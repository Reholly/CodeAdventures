using Data.Entities;
using Data.Repositories;

namespace Server.Services.UserServices;

public class UserService : IUserService
{
    private readonly IRepository<User> _usersDb;

    public UserService(IRepository<User> usersDb) => _usersDb = usersDb;
    
    public async Task<User> CreateUserAsync(User freshUser)
    {
        var newUser = new User
        {
            Name = freshUser.Name,
            Username = freshUser.Username,
            Email = freshUser.Email,
            Surname = freshUser.Surname,
            Patronymic = freshUser.Patronymic,
            RegistrationDate = DateTime.UtcNow
        };
         
        await _usersDb.AddAsync(newUser);
        
        return newUser;
    }
    
    public async Task<User?> FindByEmail(string email)
    {
        var users = await _usersDb.GetTableAsync();
        return users.FirstOrDefault(u => u.Email == email);
    }
    
    public async Task<User> EditUserAsync(string username, string email, string name, string surname, string patronymic)
    {
        var user = await FindByEmail(email) 
                   ?? throw new NullReferenceException();

        user.Email = email;
        user.Username = username;
        user.Name = name;
        user.Surname = surname;
        user.Patronymic = patronymic;

        await _usersDb.UpdateAsync(user);
        
        return user;
    }
    
    public async Task<ICollection<User>?> GetUsersAsync() 
        => await _usersDb.GetTableAsync();

    public async Task DeleteUserAsync(User user)
        => await _usersDb.RemoveAsync(user);
}