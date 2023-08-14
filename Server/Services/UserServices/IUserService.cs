using Data.Entities;

namespace Server.Services.UserServices;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User?> FindByEmail(string email);
}