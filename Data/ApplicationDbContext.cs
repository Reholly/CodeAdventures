using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<User> ApplicationUsers => Set<User>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Contest> Contests => Set<Contest>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Tiding> Tidings => Set<Tiding>();
    
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}