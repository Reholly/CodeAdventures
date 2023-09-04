using AutoMapper;
using Data.Entities;
using MediatR;
using Serilog;
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
    private readonly IMapper _mapper;

    public CreateArticleHandler(
        IArticleService articleService, 
        IMapper mapper, 
        IUserService userService)
    {
        _articleService = articleService;
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<CreateArticleResponse> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindByEmail(request.AuthorEmail);
        
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
        
        Log.Information($"Article with id {article.Id} has been created");
        
        return new CreateArticleResponse { CreatedArticle = articleModel };
    }
}