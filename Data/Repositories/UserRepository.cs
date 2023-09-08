using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public sealed class UserRepository : RepositoryBase<User>
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    protected override List<User> Entities => Context.ApplicationUsers
        .Include(user => user.WrittenArticles)
        .Include(user => user.WrittenContests)
        .Include(user => user.WrittenTests)
        .ToList();

    public override async ValueTask<User?> GetAsync(int id)
        => Entities.Find(user => user.Id == id);
}