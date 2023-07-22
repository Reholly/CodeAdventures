using Data.Entities;
using MediatR;
using Server.Services;
using Shared.Requests;
using Shared.Results;

namespace Server.Handlers;

public class CreateArticleHandler : IRequestHandler<CreateArticleRequest, CreateArticleResult>
{
    private readonly ArticlesService _articlesService;

    public CreateArticleHandler(ArticlesService articlesService)
    {
        _articlesService = articlesService;
        //d
    }
    
    public async Task<CreateArticleResult> Handle(CreateArticleRequest request, CancellationToken cancellationToken)
    {
        //здесь по сути надо производить Article фабрикой.
        var article = new Article()
        {
            //AuthorId = request.AuthorId,
            Description = request.Description,
            Text = request.Text,
            Title = request.Title,
            EditDate = null,
            PublicationDate = DateTime.UtcNow
        };

        return new CreateArticleResult();
    }
}