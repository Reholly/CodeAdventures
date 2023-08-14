using MediatR;
using Shared.Responses;
using Shared.Responses.Articles;

namespace Shared.Requests.Articles;

public record GetArticlesRequest : IRequest<GetArticlesResponse>
{
    //public required int Page { get; init; }
}