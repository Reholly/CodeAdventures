using Refit;
using Shared.DTO;

namespace Shared.Responses.Articles;

public class CreateArticleResponse
{
    public required ArticleModel? CreatedArticle { get; init; }
}