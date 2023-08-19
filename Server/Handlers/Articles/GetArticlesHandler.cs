using AutoMapper;
using MediatR;
using Server.Services.ArticleServices;
using Shared.DTO;
using Shared.Requests.Articles;
using Shared.Responses.Articles;

namespace Server.Handlers.Articles;

public class GetArticlesHandler : IRequestHandler<GetArticlesRequest, GetArticlesResponse>
{
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public GetArticlesHandler(IArticleService articleService, IMapper mapper)
    {
        _articleService = articleService;
        _mapper = mapper;
    }

    public async Task<GetArticlesResponse> Handle(GetArticlesRequest request, CancellationToken cancellationToken)
    {
        var articles = await _articleService.GetArticlesAtPage(1);//request.Page);
        var articleModels = articles.Select(_mapper.Map<ArticleModel>).ToArray();
        
        return new GetArticlesResponse { Articles = articleModels };
    }
}