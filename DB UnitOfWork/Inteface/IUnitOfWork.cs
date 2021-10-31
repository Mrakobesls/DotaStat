using DataBase.Model;
using DB_Repositories;
using DotaStat.Data.EntityFramework.Model;

namespace DB_UnitOfWork.Inteface
{
    public interface IUnitOfWork
    {
        public CurrentWinrateAllyRepository HeroAllyWRs { get; }
        public CurrentWinrateEnemyRepository HeroEnemyWRs { get; }
        public GenericRepository<Hero> Heroes { get; }
        public GenericRepository<Item> Items { get; }
        public UserRepository Users { get; }
        public GenericRepository<UserRole> UserRoles { get; }
        public GenericRepository<WeeklyWinrate> WeeklyWRs { get; }
        public GenericRepository<WeekPatch> WeekPatchs { get; }
        void SaveChanges();
    }
}
