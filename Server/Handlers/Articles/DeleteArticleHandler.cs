using MediatR;
using Server.Services.ArticleServices;
using Server.Services.AuthServices;
using Server.Services.UserServices;
using Shared.Requests.Articles;
using Shared.Responses.Articles;

namespace Server.Handlers.Articles;

public class DeleteArticleHandler : IRequestHandler<DeleteArticleRequest, DeleteArticleResponse>
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IArticleService _articleService;

    public DeleteArticleHandler(
        IAuthService authService, 
        IUserService userService, 
        IArticleService articleService)
    {
        _authService = authService;
        _userService = userService;
        _articleService = articleService;
    }

    public async Task<DeleteArticleResponse> Handle(DeleteArticleRequest request, CancellationToken cancellationToken)
    {
        if (request.Token is null)
        {
            throw new Exception();
        }

        var userPrincipal = await _authService.GetCurrentUserFromToken(request.Token);
        var userEmail = userPrincipal.FindFirst("email")!.Value;
        var user = await _userService.FindByEmail(userEmail) ?? throw new Exception();;
        
        if (userPrincipal.IsInRole("Student") && request.Article.AuthorId != user.Id)
        {
            throw new Exception();
        }

        var article = await _articleService.GetArticle(request.Article.Id) ?? throw new Exception();
        await _articleService.DeleteArticle(article);
        return new DeleteArticleResponse();
    }
}