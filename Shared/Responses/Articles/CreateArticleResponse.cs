using Refit;
using Shared.DTO;

namespace Shared.Responses.Articles;

public record CreateArticleResponse
{
    public required ArticleModel CreatedArticle { get; init; }
}