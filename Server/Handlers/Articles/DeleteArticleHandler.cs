using MediatR;
using Server.Exceptions;
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
        try
        {
            if (request.Token is null)
            {
                throw new AuthOperationException("JWT token was null");
            }

            var userPrincipal = await _authService.GetCurrentUserFromToken(request.Token);
            var userEmail = userPrincipal.FindFirst("email")!.Value;
            var user = await _userService.FindByEmail(userEmail) ??
                       throw new ServiceInvalidOperationException("Current user is null (WTF?)");
            
            if (userPrincipal.IsInRole("Student") && request.Article.AuthorId != user.Id)
            {
                throw new AuthOperationException("You aren't creator of this article");
            }

            var article = await _articleService.GetArticle(request.Article.Id) ?? throw new Exception();
            await _articleService.DeleteArticle(article);
            return new DeleteArticleResponse { IsSucceeded = true };
        }
        catch (Exception ex)
        {
            return new DeleteArticleResponse { IsSucceeded = false, Errors = new List<string> { ex.Message } };
        }
    }
}