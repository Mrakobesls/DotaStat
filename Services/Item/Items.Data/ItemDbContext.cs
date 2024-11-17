using Items.Data.Models;

namespace Items.Data;

public sealed class ItemDbContext : DbContext
{
    public DbSet<Hero> Heroes { get; set; }

    public ItemDbContext(DbContextOptions<ItemDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { }
}
