using MediatR;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 using Shared.Requests.Articles;
 using Shared.Responses.Articles;

namespace Server.Controllers;
 
[ApiController]
[Route("/api/[controller]")]
public class ArticlesController : Controller
{
    private readonly IMediator _mediator;

    public ArticlesController(IMediator mediator) => _mediator = mediator;

    [AllowAnonymous]
    [HttpGet]
    public Task<GetArticlesResponse> GetArticles(
        [FromQuery] GetArticlesRequest request)
        => _mediator.Send(request);

    [AllowAnonymous]
    [HttpGet("/articles/{id}")]
    public Task<GetArticleResponse> GetArticle(
        [FromBody, FromRoute] GetArticleRequest request)
        => _mediator.Send(request);

    [Authorize(Roles = "Student, Moderator, Admin")]
    [HttpPost("/articles/create")]
    public Task<CreateArticleResponse> CreateArticle(
        [FromBody] CreateArticleRequest request) 
        => _mediator.Send(request);

    [Authorize(Roles = "Student, Moderator, Admin")]
    [HttpPut("/articles/edit/{id}")]
    public Task<EditArticleResponse> EditArticle(
        [FromBody] EditArticleRequest request)
        => _mediator.Send(request with { Token = Request.Headers.Authorization });

    [Authorize(Roles = "Student, Moderator, Admin")]
    [HttpDelete("/articles/delete/{id}")]
    public Task<DeleteArticleResponse> DeleteArticle(
        [FromRoute, FromQuery] DeleteArticleRequest request)
        => _mediator.Send(request with { Token = Request.Headers.Authorization });
}