using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;
 
    [HttpPost("login")]
    public Task<LogInResponse> LogIn(
        [FromBody] LogInRequest request) 
        => _mediator.Send(request);

    [Authorize]
    [HttpPost("logout")]
    public Task<LogOutResponse> LogOut(
        [FromBody] LogOutRequest request)
        => _mediator.Send(request);

    [HttpPost("register")]
    public Task<CreateUserResponse> Register(
        [FromBody] CreateUserRequest request) 
        => _mediator.Send(request);
}