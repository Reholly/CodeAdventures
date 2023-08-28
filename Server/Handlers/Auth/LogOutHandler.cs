using MediatR;
using Serilog;
using Server.Services.AuthServices;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Server.Handlers.Auth;

public class LogOutHandler : IRequestHandler<LogOutRequest, LogOutResponse>
{
    private readonly IAuthService _authService;
    
    public LogOutHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LogOutResponse> Handle(LogOutRequest request, CancellationToken cancellationToken)
    {
        await _authService.LogOutAsync();
        Log.Information($"User with email {request.Email} have been logged out successfully");
        return new LogOutResponse();
    }
}