using Refit;
using Shared.Requests.Articles;
using Shared.Responses.Articles;

namespace Client.ControllerClients;

public interface IArticlesControllerClient
{
    [Get("/articles")]
    Task<ApiResponse<GetArticlesResponse>> GetArticles(GetArticlesRequest request);

    [Get("/articles/{request.Id}")]
    Task<ApiResponse<GetArticleResponse>> GetArticle(GetArticleRequest request);

    [Post("/articles")]
    Task<ApiResponse<CreateArticleResponse>> CreateArticle(CreateArticleRequest request);

    [Put("/articles/{request.Id}")]
    Task<ApiResponse<EditArticleResponse>> EditArticle(EditArticleRequest request);

    [Delete("/articles/{request.Id}")]
    Task<ApiResponse<DeleteArticleResponse>> DeleteArticle(DeleteArticleRequest request);
}