using Data.Entities;

namespace Server.Services.UserServices;

public interface IUserService
{
    /// <summary>
    /// Creates user in system.Do not register him in authService
    /// </summary>
    /// <param name="freshUser"></param>
    /// <returns></returns>
    Task<User> CreateUserAsync(User user);
    
    /// <summary>
    /// Finds user by email. Returns null if user does not exist.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<User?> FindByEmail(string email);
    
    /// <summary>
    /// Edit user by parameters. Throw NullReferenceException if user did not find
    /// </summary>
    /// <param name="username"></param>
    /// <param name="email"></param>
    /// <param name="name"></param>
    /// <param name="surname"></param>
    /// <param name="patronymic"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    Task<User> EditUserAsync(string username, string email, string name, string surname, string patronymic);
    
    /// <summary>
    /// Returns all users. Returns null if users does not exist.
    /// </summary>
    /// <returns></returns>
    Task<ICollection<User>?> GetUsersAsync();
    
    /// <summary>
    /// Deletes user forever.
    /// </summary>
    /// <param name="user"></param>
    Task DeleteUserAsync(User user);
}