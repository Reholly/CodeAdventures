using Data.Entities;

namespace Data.Repositories;

public sealed class ContestRepository : RepositoryBase<Contest>
{
    public ContestRepository(ApplicationDbContext context) : base(context) { }

    protected override List<Contest> Entities => Context.Contests
        .ToList();

    public override async ValueTask<Contest?> GetAsync(int id)
        => Entities.Find(contest => contest.Id == id);
}