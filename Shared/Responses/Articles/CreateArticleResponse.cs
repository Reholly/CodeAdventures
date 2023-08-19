using Shared.DTO;

namespace Shared.Responses.Articles;

public class CreateArticleResponse
{
    public required ArticleModel? CreatedArticle { get; init; }
    public required bool IsSucceeded { get; init; }
    public ICollection<string> Errors { get; init; } = null!;
}