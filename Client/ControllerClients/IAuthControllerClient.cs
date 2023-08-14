using Refit;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Client.ControllerClients;

public interface IAuthControllerClient
{
    [Post("/auth/register")]
    Task<CreateUserResponse> RegisterUser(CreateUserRequest request);

    [Post("/auth/login")]
    Task<LogInResponse> LogIn(LogInRequest request);
    
    [Post("/auth/logout")]
    Task<LogOutResponse> LogOut(LogOutRequest request);
}