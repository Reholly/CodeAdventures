using Data.Entities;
using Data.Repositories;

namespace Server.Services.ArticleServices;

public class ArticleService : IArticleService
{
    private readonly IRepository<Article> _articlesRepository;
    private readonly int pageArticlesCount = 15; // заменить на конфиг потом

    public ArticleService(IRepository<Article> articlesRepository)
    {
        _articlesRepository = articlesRepository;
    }
    
    public async Task<ICollection<Article>> GetArticles(int page)
    {
        var articles = await _articlesRepository.GetTableAsync();
        return articles;/*
            .Skip(page * pageArticlesCount)
            .Take(articles.Count - page * pageArticlesCount)
            .ToArray();*/
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