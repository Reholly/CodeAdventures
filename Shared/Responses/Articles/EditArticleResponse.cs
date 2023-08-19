using Shared.DTO;

namespace Shared.Responses.Articles;

public record EditArticleResponse
{
    public required ArticleModel EditedArticle { get; init; }
    public required bool IsSucceeded { get; init; }
    public ICollection<string> Errors { get; init; } = null!;
}