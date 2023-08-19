using AutoMapper;
using Data.Entities;
using MediatR;
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
        try
        {
            var article = new Article
            {
                Author = (await _userService.FindById(request.AuthorId))!,
                Description = request.Description,
                EditDate = request.CreatingTime,
                PublicationDate = DateTime.Now,
                Title = request.Title,
                Text = request.Text
            };
            await _articleService.CreateArticle(article);
            var articleModel = _mapper.Map<ArticleModel>(article);
            return new CreateArticleResponse { CreatedArticle = articleModel, IsSucceeded = true };
        }
        catch (Exception e)
        {
            return new CreateArticleResponse
            {
                CreatedArticle = null,
                IsSucceeded = false,
                Errors = new[] { e.Message }
            };
        }
    }
}