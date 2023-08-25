using AutoMapper;
using Data.Entities;
using MediatR;
using Server.Exceptions;
using Server.Services;
using Server.Services.ArticleServices;
using Server.Services.UserServices;
using Shared.DTO;
using Shared.Requests.Articles;
using Shared.Responses.Articles;

namespace Server.Handlers.Articles;

public class CreateArticleHandler : IRequestHandler<CreateArticleRequest, CreateArticleResponse>
{
    private readonly IArticleService _articleService;
    private readonly IUserService _userService;
    private readonly TokenParseService _tokenParseService;
    private readonly IMapper _mapper;

    public CreateArticleHandler(
        IArticleService articleService, 
        IMapper mapper, 
        IUserService userService, 
        TokenParseService tokenParseService)
    {
        _articleService = articleService;
        _mapper = mapper;
        _userService = userService;
        _tokenParseService = tokenParseService;
    }
    
    public async Task<CreateArticleResponse> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        var userClaims = request.Token is not null ? 
            _tokenParseService.ParseClaimsPrincipalFromJwt(request.Token)
            : throw new AuthOperationException("JWT Token отсутствует");
        
        var user = await _userService.FindByEmail(userClaims.FindFirst("email")!.Value);
        
        var article = new Article
        {
            Author = user!,
            Description = request.Description,
            EditDate = request.CreatingTime,
            PublicationDate = request.CreatingTime,
            Title = request.Title,
            Text = request.Text
        };
        
        await _articleService.CreateArticle(article);
        var articleModel = _mapper.Map<ArticleModel>(article);
        
        return new CreateArticleResponse { CreatedArticle = articleModel };
    }
}