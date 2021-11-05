using DotaStat.Data.EntityFramework.Model;
using DotaStat.Data.EntityFramework.Repositories;

namespace DotaStat.Data.EntityFramework.UnitOfWork
{
    public interface IUnitOfWork
    {
        public CurrentWinrateAllyRepository HeroAllyWRs { get; }
        public CurrentWinrateEnemyRepository HeroEnemyWRs { get; }
        public HeroRepository Heroes { get; }
        public GenericRepository<Item> Items { get; }
        public UserRepository Users { get; }
        public GenericRepository<UserRole> UserRoles { get; }
        public GenericRepository<WeeklyWinrate> WeeklyWRs { get; }
        public WeekPatchesRepository WeekPatches { get; }
        void SaveChanges();
        void DetachAllEntities();
    }
}
