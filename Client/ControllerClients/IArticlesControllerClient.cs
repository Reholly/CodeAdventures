using Refit;
using Shared.DTO;
using Shared.Requests.Articles;
using Shared.Responses.Articles;

namespace Client.ControllerClients;

public interface IArticlesControllerClient
{
    [Get("/articles")]
    Task<GetArticlesResponse> GetArticles(GetArticlesRequest request);

    [Get("/articles/{id}")]
    Task<ArticleModel> GetArticle(int id);

    [Post("/articles/create")]
    Task CreateArticle([Body] ArticleModel articleModel);

    [Put("/articles/edit/{id}")]
    Task<ArticleModel> EditArticle(int id);

    [Delete("/articles/delete/{id}")]
    Task DeleteArticle(int id);
}