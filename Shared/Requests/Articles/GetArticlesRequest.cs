using MediatR;
using Shared.Responses.Articles;

namespace Shared.Requests.Articles;

public record GetArticlesRequest : IRequest<GetArticlesResponse>;