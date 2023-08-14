using Data.Entities;

namespace Server.Services.ArticleServices;

public interface IArticleService
{
   Task<ICollection<Article>> GetArticles(int page);
   Task<Article> GetArticle(int id);
   Task<Article> CreateArticle(Article article);
   Task<Article> EditArticle(Article article);
   Task DeleteArticle(Article article);
}