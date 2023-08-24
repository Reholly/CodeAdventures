using AutoMapper;
using MediatR;
using Server.Services.ArticleServices;
using Server.Services.UserServices;
using Shared.DTO;
using Shared.Requests.Articles;
using Shared.Responses.Articles;

namespace Server.Handlers.Articles;

public class EditArticleHandler : IRequestHandler<EditArticleRequest, EditArticleResponse>
{
    private readonly IUserService _userService;
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public EditArticleHandler(
        IUserService userService,
        IArticleService articleService,
        IMapper mapper)
    {
        _userService = userService; 
        _articleService = articleService;
        _mapper = mapper;
    }

    public async Task<EditArticleResponse> Handle(EditArticleRequest request, CancellationToken cancellationToken)
    {
        await _articleService.EditArticle(request.Id, request.Title, request.Text, request.Description);
        
        var article = await _articleService.GetArticle(request.Id);
        var articleModel = _mapper.Map<ArticleModel>(article);
        
        return new EditArticleResponse { EditedArticle = articleModel };
    }
}