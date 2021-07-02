using DataBase.Model;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class CurrentDbContext : DbContext
    {
        public DbSet<WeeklyHeroWinRate> WeeklyHeroWinRates { get; set; }
        public DbSet<LastWeekHeroWinRate> LastWeekHeroWinRates { get; set; }
        public DbSet<WeekPatch> WeekPatches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Hero> Heroes { get; set; }     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MRAKOBESPC\\SQLEXPRESS01;DataBase=DotaStat;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LastWeekHeroWinRate>()
                .HasKey(i => new { i.HeroIdMain, i.HeroIdCompareWith });
            modelBuilder.Entity<WeeklyHeroWinRate>()
                .HasKey(i => new { i.WeekPatchId, i.HeroId });

        }
    }
}
