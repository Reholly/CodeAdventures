using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public sealed class ArticleRepository : RepositoryBase<Article>
{
    public ArticleRepository(ApplicationDbContext context) : base(context) { }

    protected override List<Article> Entities => Context.Articles
        .Include(article => article.Author)
        .ToList();

    public override async ValueTask<Article?> GetAsync(int id)
        => Entities.Find(article => article.Id == id);
}