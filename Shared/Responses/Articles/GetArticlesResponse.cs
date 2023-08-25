using Shared.DTO;

namespace Shared.Responses.Articles;

public record GetArticlesResponse
{
    public required ICollection<ArticleModel> Articles { get; init; }
}