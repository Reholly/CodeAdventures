using AutoMapper;
using Data.Entities;
using MediatR;
using Server.Services.AuthServices;
using Server.Services.UserServices;
using Shared.DTO;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Server.Handlers.Auth;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    
    public CreateUserHandler(IMapper mapper, IUserService userService, IAuthService authService)
    {
        _mapper = mapper;
        _userService = userService;
        _authService = authService;
    }
    
    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var model = request.RegisterModel;
        var registeredUser = _mapper.Map<RegisterModel, User>(model);
        var user = await _authService.RegisterUserAsync(registeredUser, model.Password);
        
        //var result = await _authService.LogInUserAsync(user);
        var userDto = _mapper.Map<User, UserModel>(registeredUser);
        
        return new CreateUserResponse
        {
            UserModel =  userDto, 
            IsSucceeded = user.Succeeded,
            Errors = user.Errors.Select(e => e.Description)
        };
    }
}