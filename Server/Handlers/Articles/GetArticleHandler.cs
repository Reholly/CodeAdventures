using AutoMapper;
using MediatR;
using Server.Services.ArticleServices;
using Shared.DTO;
using Shared.Requests.Articles;
using Shared.Responses.Articles;

namespace Server.Handlers.Articles;

public class GetArticleHandler : IRequestHandler<GetArticleRequest, GetArticleResponse>
{
    private readonly IMapper _mapper;
    private readonly IArticleService _articleService;

    public GetArticleHandler(IMapper mapper, IArticleService articleService)
    {
        _mapper = mapper;
        _articleService = articleService;
    }

    public async Task<GetArticleResponse> Handle(GetArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await _articleService.GetArticle(request.Id);
        var articleModel = _mapper.Map<ArticleModel>(article);
        
        return new GetArticleResponse { Article = articleModel };
    }
}