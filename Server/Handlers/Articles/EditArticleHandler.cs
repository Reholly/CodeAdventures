using AutoMapper;
using MediatR;
using Server.Services.ArticleServices;
using Server.Services.AuthServices;
using Server.Services.UserServices;
using Shared.DTO;
using Shared.Requests.Articles;
using Shared.Responses.Articles;

namespace Server.Handlers.Articles;

public class EditArticleHandler : IRequestHandler<EditArticleRequest, EditArticleResponse>
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public EditArticleHandler(
        IUserService userService, 
        IAuthService authService, 
        IArticleService articleService, 
        IMapper mapper)
    {
        _userService = userService;
        _authService = authService;
        _articleService = articleService;
        _mapper = mapper;
    }

    public async Task<EditArticleResponse> Handle(EditArticleRequest request, CancellationToken cancellationToken)
    {
        if (request.Token is null)
        {
            throw new Exception();
        }

        var userPrincipal = await _authService.GetCurrentUserFromToken(request.Token);
        var userEmail = userPrincipal.FindFirst("email")!.Value;
        var user = await _userService.FindByEmail(userEmail);
        
        if (user is null)
        {
            throw new ArgumentException();
        }
        
        if (userPrincipal.IsInRole("Student") && request.Article.AuthorId != user.Id)
        {
            throw new InvalidOperationException();
        }
        
        await _articleService.EditArticle(request.Article.Id, request.Title, request.Text, request.Description);
        var article = await _articleService.GetArticle(request.Article.Id);
        var articleModel = _mapper.Map<ArticleModel>(article);
        return new EditArticleResponse { EditedArticle = articleModel };
    }
}