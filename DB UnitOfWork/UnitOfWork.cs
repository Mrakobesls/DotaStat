using DataBase.Model;
using DB_Repositories;
using DB_UnitOfWork.Inteface;
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

        private HeroRepository _heroes;
        public HeroRepository Heroes => _heroes ??= new HeroRepository(_context);

        private GenericRepository<Item> _items;
        public GenericRepository<Item> Items => _items ??= new GenericRepository<Item>(_context);

        private UserRepository _users;
        public UserRepository Users => _users ??= new UserRepository(_context);

        private GenericRepository<UserRole> _userRoles;
        public GenericRepository<UserRole> UserRoles => _userRoles ??= new GenericRepository<UserRole>(_context);

        private GenericRepository<WeeklyWinrate> _weeklyWRs;
        public GenericRepository<WeeklyWinrate> WeeklyWRs => _weeklyWRs ??= new GenericRepository<WeeklyWinrate>(_context);

        private WeekPatchesRepository _weekPatchs;
        public WeekPatchesRepository WeekPatches => _weekPatchs ??= new WeekPatchesRepository(_context);

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
