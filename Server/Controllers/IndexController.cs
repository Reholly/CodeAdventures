using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests.Index;
using Shared.Responses.Index;

namespace Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class IndexController : Controller
{
    private readonly IMediator _mediator;

    public IndexController(IMediator mediator) => _mediator = mediator;

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public Task<CreateTidingResponse> CreateTiding(
        [FromBody] CreateTidingRequest request) 
        => _mediator.Send(request);

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public Task<DeleteTidingResponse> DeleteTiding(
        [FromBody] DeleteTidingRequest request)
        => _mediator.Send(request);
    
    [AllowAnonymous]
    [HttpGet]
    public Task<GetAllTidingsResponse> GetAllTidings(
        [FromBody] GetAllTidingsRequest request)
        => _mediator.Send(request);
    
    [AllowAnonymous]
    [HttpGet("/last")]
    public Task<GetLastTidingResponse> GetLastTiding(
        [FromBody] GetLastTidingRequest request)
        => _mediator.Send(request);

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public Task<UpdateTidingResponse> UpdateTiding(
        [FromBody] UpdateTidingRequest request)
        => _mediator.Send(request);
}