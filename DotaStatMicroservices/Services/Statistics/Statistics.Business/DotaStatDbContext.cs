using DotaStat.Data.EntityFramework.Model;
using Microsoft.EntityFrameworkCore;

namespace DotaStat.Data.EntityFramework
{
    public class DotaStatDbContext : DbContext
    {
        public DbSet<WeeklyWinrate> WeeklyHeroWinRates { get; set; }
        public DbSet<CurrentWinrateAlly> CurrentWinrateAllies { get; set; }
        public DbSet<CurrentWinrateEnemy> CurrentWinrateEnemies { get; set; }
        public DbSet<WeekPatch> WeekPatches { get; set; }
        // public DbSet<User> Users { get; set; }
        // public DbSet<UserRole> UserRoles { get; set; }
        // public DbSet<Item> Items { get; set; }
        public DbSet<Hero> Heroes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentWinrateAlly>()
                .HasKey(i => new {HeroId1 = i.MainHero, HeroId2 = i.ComparedHero });
            modelBuilder.Entity<CurrentWinrateEnemy>()
                .HasKey(i => new {HeroId1 = i.MainHero, HeroId2 = i.ComparedHero });

            modelBuilder.Entity<WeeklyWinrate>()
                .HasKey(i => new { i.WeekPatchId, i.HeroId });

            modelBuilder.Entity<CurrentWinrateAlly>()
                .HasOne(c => c.HeroMain)
                .WithMany(h => h.CurrentWinrateAlliesMain)
                .HasForeignKey(c => c.MainHero)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CurrentWinrateAlly>()
                .HasOne(c => c.HeroCompareWith)
                .WithMany(h => h.CurrentWinrateAlliesCompared)
                .HasForeignKey(c => c.ComparedHero)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CurrentWinrateEnemy>()
                .HasOne(c => c.HeroMain)
                .WithMany(h => h.CurrentWinrateEnemiesMain)
                .HasForeignKey(c => c.MainHero)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CurrentWinrateEnemy>()
                .HasOne(c => c.HeroCompareWith)
                .WithMany(h => h.CurrentWinrateEnemiesCompared)
                .HasForeignKey(c => c.ComparedHero)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<CurrentWinrateEnemy>()
            //                .HasOne(r => r.HeroMain)
            //                .WithMany(r => r.CurrentWinrateEnemies);

        }
    }
}
