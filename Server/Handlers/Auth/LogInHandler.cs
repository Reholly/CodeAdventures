using AutoMapper;
using Data.Entities;
using MediatR;
using Server.Exceptions;
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
        
        if (loggedInResponse.IsSucceeded == false)
        {
            throw new AuthOperationException(loggedInResponse.Errors.ToArray()[0]);
        }
        
        var user = await _userService.FindByEmail(loginModel.Email)
            ?? throw new ServiceInvalidOperationException("Пользователя с таким email не существует");

        var userDto = _mapper.Map<User, UserModel>(user);
        
        return new LogInResponse
        {
            AuthenticatedUser = userDto,
            Token = new TokenModel
            {
                Token = loggedInResponse.Token,
                ExpireTime = loggedInResponse.TokenExpireDate,
            }
        };
    }
}