using Shared.DTO;

namespace Shared.Responses.Articles;

public record GetArticleResponse
{
    public required ArticleModel Article { get; init; }
}