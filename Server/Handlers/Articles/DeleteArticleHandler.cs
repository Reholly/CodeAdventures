using MediatR;
using Server.Exceptions;
using Server.Services.ArticleServices;
using Shared.Requests.Articles;
using Shared.Responses.Articles;

namespace Server.Handlers.Articles;

public class DeleteArticleHandler : IRequestHandler<DeleteArticleRequest, DeleteArticleResponse>
{
    private readonly IArticleService _articleService;

    public DeleteArticleHandler(IArticleService articleService) => _articleService = articleService;
  
    public async Task<DeleteArticleResponse> Handle(DeleteArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await _articleService.GetArticle(request.Article.Id) 
                      ?? throw new ServiceInvalidOperationException("Статьи с таким id не существует");
        await _articleService.DeleteArticle(article);
        
        return new DeleteArticleResponse();
    }
}