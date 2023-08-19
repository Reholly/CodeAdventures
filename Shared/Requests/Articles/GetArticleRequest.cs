using MediatR;
using Shared.Responses.Articles;

namespace Shared.Requests.Articles;

public record GetArticleRequest : IRequest<GetArticleResponse>
{
    public required int Id { get; init; }
}