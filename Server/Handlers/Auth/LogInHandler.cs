using AutoMapper;
using Data.Entities;
using MediatR;
using Server.Services.AuthServices;
using Server.Services.UserServices;
using Shared.DTO;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Server.Handlers.Auth;

public class LogInHandler : IRequestHandler<LogInRequest, LogInResponse>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    
    public LogInHandler(IAuthService authService, IUserService userService, IMapper mapper)
    {
        _authService = authService;
        _userService = userService;
        _mapper = mapper;
    }
    
    public async Task<LogInResponse> Handle(LogInRequest request, CancellationToken cancellationToken)
    {
        var loginModel = request.LoginModel;
        var loggedInResponse = await _authService.LogInUserAsync(loginModel.Email, loginModel.Password);
        var user = await _userService.FindByEmail(loginModel.Email);

        var userDto = _mapper.Map<User, UserModel>(user);
        
        return new LogInResponse
        {
            AuthenticatedUser = userDto,
            Token = new TokenModel
            {
                Token = loggedInResponse.Token,
                ExpireTime = loggedInResponse.TokenExpireDate,
            },
            IsSucceeded = loggedInResponse.IsSucceeded,
            Errors = loggedInResponse.Errors
        };
    }
}