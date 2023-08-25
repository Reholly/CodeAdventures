using Shared.DTO;

namespace Shared.Responses.Articles;

public class GetArticlesResponse
{
    public required ICollection<ArticleModel> Articles { get; init; }
}