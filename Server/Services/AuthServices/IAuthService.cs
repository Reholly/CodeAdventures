using System.Security.Claims;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Server.Services.AuthServices.AuthResponses;

namespace Server.Services.AuthServices;

public interface IAuthService
{
    /// <summary>
    /// Log in for user. Returns response of logging in. May returns string-type errors
    /// with description. Should use them for the transfer on front-end to user.
    /// Can throw NullReferenceException.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<LogInResponse> LogInUserAsync(string email, string password);
    
    /// <summary>
    /// Register user in system, but not adding him to DB (use IUserService for this).
    /// Can throw InvalidOperationException if user is already exist in system.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// /// <exception cref="InvalidOperationException"></exception>
    Task<IdentityResult> RegisterUserAsync(User user, string password);
    
    /// <summary>
    /// Log out for user. 
    /// </summary>
    /// <returns></returns>
    Task LogOutAsync();

    /// <summary>
    /// Gets claims principal of current user from JWT token.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ClaimsPrincipal> GetCurrentUserFromToken(string token);
}