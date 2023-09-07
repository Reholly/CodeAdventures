using MediatR;
using Shared.Responses.Articles;

namespace Shared.Requests.Articles;

public record CreateArticleRequest : IRequest<CreateArticleResponse>  
{
    public required string AuthorEmail { get; init; }
    public required string Title { get; init; } 
    public required string Description { get; init; } 
    public required string Text { get; init; }
    public DateTimeOffset CreatingTime { get; init; }
}