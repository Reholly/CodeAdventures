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
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<LogInResponse> LogIn(
        [FromBody] LogInRequest request) =>
        await _mediator.Send(request);

    [HttpPost("logout")]
    public async Task<LogOutResponse> LogOut(
        [FromBody] LogOutRequest request)
        => await _mediator.Send(request);

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<CreateUserResponse> Register(
        [FromBody] CreateUserRequest request) 
        => await _mediator.Send(request);
}