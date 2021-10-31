using DataBase;
using DataBase.Model;
using DB_Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DotaStat.Data.EntityFramework;
using DotaStat.Data.EntityFramework.Model;

namespace DB_Repositories
{
    public class CurrentWinrateEnemyRepository : IRead<CurrentWinrateEnemy>, IUpdate<CurrentWinrateEnemy>
    {
        protected readonly DotaStatDbContext _context;
        public CurrentWinrateEnemyRepository(DotaStatDbContext context)
        {
            _context = context;
        }

        public CurrentWinrateEnemy Read(params int[] id)
        {
            return _context.Find<CurrentWinrateEnemy>(id);
        }
        public IQueryable<CurrentWinrateEnemy> ReadAll()
        {
            return _context.Set<CurrentWinrateEnemy>();
        }
        public CurrentWinrateEnemy Update(CurrentWinrateEnemy newEnt)
        {
            return _context.Update(newEnt).Entity;
        }
    }
}
