using Shared.DTO;

namespace Shared.Responses.Articles;

public class CreateArticleResponse
{
    public required ArticleModel? CreatedArticle { get; init; }
    public required bool IsSucceeded { get; init; }
    public required ICollection<string> Errors { get; init; } = new List<string>();
}