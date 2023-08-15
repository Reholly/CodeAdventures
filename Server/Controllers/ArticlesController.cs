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
        
    [Authorize(Roles = "Student")]
    [HttpGet]
    public async Task<GetArticlesResponse> GetArticles(
        [FromQuery] GetArticlesRequest request)
        => await _mediator.Send(request);
    /*
    [HttpGet("/articles/{id}")]
    Task<ArticleModel> GetArticle(int id)
    {
        
    }

    [HttpPost("/articles/create")]
    Task CreateArticle(ArticleModel articleModel)
    {
        
    }

    [HttpPut("/articles/edit/{id}")]
    Task<ArticleModel> EditArticle(int id)
    {
        
    }

    [HttpDelete("/articles/delete/{id}")]
    Task DeleteArticle(int id)
    {
        
    }*/
}
