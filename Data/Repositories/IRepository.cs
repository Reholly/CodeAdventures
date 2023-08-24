using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    ValueTask<TEntity?> GetAsync(int id);
    Task<DbSet<TEntity>> GetTableAsync();
    Task AddAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
}