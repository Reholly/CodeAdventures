using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public sealed class TestRepository : RepositoryBase<Test>
{
    public TestRepository(ApplicationDbContext context) : base(context) { }

    protected override List<Test> Entities => Context.Tests
        .Include(test => test.Questions)
        .Include(test => test.Author)
        .ToList();

    public override async ValueTask<Test?> GetAsync(int id)
        => Entities.Find(test => test.Id == id);
}