using DataBase;
using DataBase.Model;
using DB_UnitOfWork.Inteface;
using DB_Repositories;
using DotaStat.Data.EntityFramework;
using DotaStat.Data.EntityFramework.Model;

namespace DB_UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DotaStatDbContext _context;

        private CurrentWinrateAllyRepository _heroAllyWRs;
        public CurrentWinrateAllyRepository HeroAllyWRs => _heroAllyWRs ??= new CurrentWinrateAllyRepository(_context);

        private CurrentWinrateEnemyRepository _heroEnemyWRs;
        public CurrentWinrateEnemyRepository HeroEnemyWRs => _heroEnemyWRs ??= new CurrentWinrateEnemyRepository(_context);

        private GenericRepository<Hero> _heroes;
        public GenericRepository<Hero> Heroes => _heroes ??= new GenericRepository<Hero>(_context);

        private GenericRepository<Item> _items;
        public GenericRepository<Item> Items => _items ??= new GenericRepository<Item>(_context);

        private UserRepository _users;
        public UserRepository Users => _users ??= new UserRepository(_context);

        private GenericRepository<UserRole> _userRoles;
        public GenericRepository<UserRole> UserRoles => _userRoles ??= new GenericRepository<UserRole>(_context);

        private GenericRepository<WeeklyWinrate> _weeklyWRs;
        public GenericRepository<WeeklyWinrate> WeeklyWRs => _weeklyWRs ??= new GenericRepository<WeeklyWinrate>(_context);

        private GenericRepository<WeekPatch> _weekPatchs;
        public GenericRepository<WeekPatch> WeekPatchs => _weekPatchs ??= new GenericRepository<WeekPatch>(_context);

        public UnitOfWork(DotaStatDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
