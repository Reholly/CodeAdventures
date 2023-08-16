using Data.Entities;

namespace Server.Services.ArticleServices;

public interface IArticleService
{
   /// <summary>
   /// Returns articles from page. Can throw InvalidOperationException if config is empty
   /// </summary>
   /// <param name="page"></param>
   /// <returns></returns>
   /// <exception cref="InvalidOperationException"></exception>
   Task<ICollection<Article>> GetArticlesAtPage(int page);
   
   /// <summary>
   /// Returns all articles.
   /// </summary>
   /// <returns></returns>
   Task<ICollection<Article>> GetArticles();
   
   /// <summary>
   /// Returns changed article and can throw NulLReferenceException if article does not exist
   /// </summary>
   /// <param name="id"></param>
   /// <param name="title"></param>
   /// <param name="text"></param>
   /// <param name="description"></param>
   /// <returns></returns>
   /// <exception cref="NullReferenceException"></exception>
   Task<Article> EditArticle(int id, string title, string text, string description);
   
   /// <summary>
   /// Adding article in system.
   /// </summary>
   /// <param name="article"></param>
   Task CreateArticle(Article article);
   
   /// <summary>
   /// Deleting article forever.
   /// </summary>
   /// <param name="article"></param>
   Task DeleteArticle(Article article);
}