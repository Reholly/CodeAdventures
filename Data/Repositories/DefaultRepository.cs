using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class DefaultRepository<TEntity> : IRepository<TEntity> 
    where TEntity : class
{
    private readonly DbContext _context;

    public DefaultRepository(DbContext context)
    {
        _context = context;
    }

    public async ValueTask<TEntity?> GetAsync(int id) => await _context.Set<TEntity>().FindAsync(id);

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    { 
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    { 
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
}