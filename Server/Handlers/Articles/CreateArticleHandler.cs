using MediatR;
using Server.Services.ArticleServices;
using Shared.Requests.Articles;
using Shared.Responses.Articles;

namespace Server.Handlers.Articles;

public class CreateArticleHandler : IRequestHandler<CreateArticleRequest, CreateArticleResponse>
{
    private readonly IArticleService _articleService;

    public CreateArticleHandler(IArticleService articleService)
    {
        _articleService = articleService;
    }
    
    public async Task<CreateArticleResponse> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        //await _articleService.CreateArticle()
        return new CreateArticleResponse();
    }
}