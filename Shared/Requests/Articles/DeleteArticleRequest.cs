using MediatR;
using Shared.DTO;
using Shared.Responses.Articles;

namespace Shared.Requests.Articles;

public record DeleteArticleRequest : IRequest<DeleteArticleResponse>
{
    public required ArticleModel Article { get; init; }
    public string? Token { get; init; }
}