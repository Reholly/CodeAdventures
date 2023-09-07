using MediatR;
using Shared.DTO;
using Shared.Responses.Articles;

namespace Shared.Requests.Articles;

public record DeleteArticleRequest : IRequest<DeleteArticleResponse>
{
    public required int Id { get; init; }
    public required ArticleModel Article { get; init; }
}