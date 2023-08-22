using System.Security.Claims;
using MediatR;
using Shared.DTO;
using Shared.Responses.Articles;

namespace Shared.Requests.Articles;

public record EditArticleRequest : IRequest<EditArticleResponse>
{
    public required int Id { get; init; }
    public required ArticleModel Article { get; init; }
    public required string Text { get; init; }
    public required string Description { get; init; }
    public required string Title { get; init; }
    public required DateTime EditingTime { get; init; }
    public string? Token { get; init; }
}