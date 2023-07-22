using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Contest> Contests => Set<Contest>();

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        string connstr = "Host=localhost;Port=5432;Database=CodeAdventures;Username=postgres;Password=12345";
        builder.UseNpgsql(connstr);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}