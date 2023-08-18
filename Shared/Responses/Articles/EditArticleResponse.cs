using Shared.DTO;

namespace Shared.Responses.Articles;

public record EditArticleResponse
{
    public required ArticleModel EditedArticle { get; init; }
}