using AutoMapper;
using Data.Entities;
using MediatR;
using Server.Services.AuthServices;
using Shared.DTO;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Server.Handlers.Auth;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    
    public CreateUserHandler(IMapper mapper, IAuthService authService)
    {
        _mapper = mapper;
        _authService = authService;
    }
    
    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var model = request.RegisterModel;
        var registeredUser = _mapper.Map<RegisterModel, User>(model);

        try
        {
            var user = await _authService.RegisterUserAsync(registeredUser, model.Password);
            
            var userDto = _mapper.Map<User, UserModel>(registeredUser);
        
            return new CreateUserResponse
            {
                CreatedUser =  userDto
            };
        }
        catch (InvalidOperationException exception)
        {
            return new CreateUserResponse
            {
                CreatedUser = null
            };
        }
        
    }
}