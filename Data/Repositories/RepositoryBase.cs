namespace Data.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext Context;
    protected abstract List<TEntity> Entities { get; }

    protected RepositoryBase(ApplicationDbContext context)
    {
        Context = context;
    }

    public abstract ValueTask<TEntity?> GetAsync(int id);

    public async Task<List<TEntity>> GetTableAsync()
        => Entities;

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        await Context.SaveChangesAsync();
    }
}