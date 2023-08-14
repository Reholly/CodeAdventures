using Shared.DTO;

namespace Shared.Responses.Articles;

public class GetArticlesResponse
{
    public ICollection<ArticleModel> Articles { get; init; } = null!;
}