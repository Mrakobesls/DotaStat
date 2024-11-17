using Microsoft.EntityFrameworkCore;

namespace Hero.Data;

public sealed class HeroesDbContext : DbContext
{
    public DbSet<Models.Hero> Heroes { get; set; }

    public HeroesDbContext(DbContextOptions<HeroesDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { }
}
