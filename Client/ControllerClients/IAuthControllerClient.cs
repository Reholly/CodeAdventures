using Refit;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Client.ControllerClients;

public interface IAuthControllerClient
{
    [Post("/auth/register")]
    Task<ApiResponse<CreateUserResponse>> RegisterUser(CreateUserRequest request);

    [Post("/auth/login")]
    Task<ApiResponse<LogInResponse>> LogIn(LogInRequest request);
    
    [Post("/auth/logout")]
    Task<ApiResponse<LogOutResponse>> LogOut(LogOutRequest request);
}