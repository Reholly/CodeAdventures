using Data.Entities;
using Data.Repositories;

namespace Server.Services.ArticleServices;

public class ArticleService : IArticleService
{
    private readonly IRepository<Article> _articlesRepository;
    private readonly IConfiguration _config;
    public ArticleService(IRepository<Article> articlesRepository, IConfiguration config)
    {
        _articlesRepository = articlesRepository;
        _config = config;
    }
    
    public async Task<ICollection<Article>> GetArticles(int page)
    {
        var articles = await _articlesRepository.GetTableAsync();
        var pageArticlesCount = int.Parse(_config.GetValue<string>("articleOnPages") ?? throw new InvalidOperationException());
        return articles
            .Skip(page * pageArticlesCount)
            .Take(articles.Count - page * pageArticlesCount)
            .ToArray();
    }

    public Task<Article> GetArticle(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Article> CreateArticle(Article article)
    {
        throw new NotImplementedException();
    }

    public Task<Article> EditArticle(Article article)
    {
        throw new NotImplementedException();
    }

    public Task DeleteArticle(Article article)
    {
        throw new NotImplementedException();
    }
}