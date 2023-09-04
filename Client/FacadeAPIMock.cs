using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.DTO;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Client;

public class FacadeApiMock
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public FacadeApiMock(ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
    {
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<LogInResponse> LogInMock(LogInRequest request)
    {
        var response = new LogInResponse { AuthenticatedUser = new UserModel(), Token = null! };
        await Task.Delay(TimeSpan.FromSeconds(3));

        var token =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        var expiry = DateTimeOffset.Now.AddHours(1);

        await _localStorage.SetItemAsync("token", token);
        await _localStorage.SetItemAsync("expiry", expiry);
        await _authenticationStateProvider.GetAuthenticationStateAsync();

        return response;
    }
    
    
}