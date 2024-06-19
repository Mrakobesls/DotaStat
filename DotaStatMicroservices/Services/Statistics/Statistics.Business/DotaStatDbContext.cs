using Microsoft.EntityFrameworkCore;
using Statistics.Business.Model;

namespace Statistics.Business
{
    public sealed class DotaStatDbContext : DbContext
    {
        public DbSet<WeeklyWinRate> WeeklyHeroWinRates { get; set; }
        public DbSet<CurrentWinRateAlly> CurrentWinrateAllies { get; set; }
        public DbSet<CurrentWinRateEnemy> CurrentWinrateEnemies { get; set; }

        public DbSet<WeekPatch> WeekPatches { get; set; }

        // public DbSet<User> Users { get; set; }
        // public DbSet<UserRole> UserRoles { get; set; }
        // public DbSet<Item> Items { get; set; }
        public DbSet<Hero> Heroes { get; set; }

        public DotaStatDbContext(DbContextOptions<DotaStatDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentWinRateAlly>()
                .HasKey(i => new { HeroId1 = i.MainHeroId, HeroId2 = i.ComparedHeroId });
            modelBuilder.Entity<CurrentWinRateEnemy>()
                .HasKey(i => new { HeroId1 = i.MainHeroId, HeroId2 = i.ComparedHeroId });

            modelBuilder.Entity<WeeklyWinRate>()
                .HasKey(i => new { i.WeekPatchId, i.HeroId });

            modelBuilder.Entity<CurrentWinRateAlly>()
                .HasOne(c => c.MainHero)
                .WithMany(h => h.CurrentWinRateAlliesMains)
                .HasForeignKey(c => c.MainHeroId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CurrentWinRateAlly>()
                .HasOne(c => c.ComparedHero)
                .WithMany(h => h.CurrentWinRateAlliesCompareds)
                .HasForeignKey(c => c.ComparedHeroId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CurrentWinRateEnemy>()
                .HasOne(c => c.MainHero)
                .WithMany(h => h.CurrentWinRateEnemiesMains)
                .HasForeignKey(c => c.MainHeroId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CurrentWinRateEnemy>()
                .HasOne(c => c.ComparedHero)
                .WithMany(h => h.CurrentWinRateEnemiesCompareds)
                .HasForeignKey(c => c.ComparedHeroId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<CurrentWinrateEnemy>()
            //                .HasOne(r => r.HeroMain)
            //                .WithMany(r => r.CurrentWinrateEnemies);
        }
    }
}
