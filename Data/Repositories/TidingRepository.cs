using Data.Entities;

namespace Data.Repositories;

public sealed class TidingRepository : RepositoryBase<Tiding>
{
    public TidingRepository(ApplicationDbContext context) : base(context) { }

    protected override List<Tiding> Entities => Context.Tidings
        .ToList();

    public override async ValueTask<Tiding?> GetAsync(int id)
        => Entities.Find(tiding => tiding.Id == id);
}