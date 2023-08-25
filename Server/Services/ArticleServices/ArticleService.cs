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
    
    public async Task<ICollection<Article>> GetArticlesAtPage(int page)
    {
        var articles = await _articlesRepository.GetTableAsync();
        var pageArticlesCount = int.Parse(_config.GetValue<string>("articlesOnPages")
                                          ?? throw new InvalidOperationException());

        return articles 
            .Skip((page - 1) * pageArticlesCount)
            .Take(pageArticlesCount)
            .ToArray();
    }

    public async Task<Article?> GetArticle(int id)
    {
        var articles = await _articlesRepository.GetTableAsync();
        
        return articles.FirstOrDefault(u => u.Id == id);
    }
       

    public async Task<Article> EditArticle(int id, string title, string text, string description)
    {
        var articles = await GetArticles();
        var currentArticle = articles.FirstOrDefault(art => art.Id == id) 
                             ?? throw new NullReferenceException();

        currentArticle.Title = title;
        currentArticle.Text = text;
        currentArticle.Description = description;

        await _articlesRepository.UpdateAsync(currentArticle);

        return currentArticle;
    }
    
    public async Task<ICollection<Article>> GetArticles() 
        => (await _articlesRepository.GetTableAsync()).ToList();
    
    public async Task CreateArticle(Article article) 
        => await _articlesRepository.AddAsync(article);

    public async Task DeleteArticle(Article article) 
        => await _articlesRepository.RemoveAsync(article);
}