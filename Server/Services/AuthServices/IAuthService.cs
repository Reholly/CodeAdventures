using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Server.Services.AuthServices.AuthResponses;

namespace Server.Services.AuthServices;

public interface IAuthService
{
    Task<LogInResponse> LogInUserAsync(string email, string password);
    Task<IdentityResult> RegisterUserAsync(User user, string password);
    Task LogOutAsync();
}