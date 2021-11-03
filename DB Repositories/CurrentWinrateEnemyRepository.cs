using DB_Repositories.Interfaces;
using DotaStat.Data.EntityFramework;
using DotaStat.Data.EntityFramework.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DB_Repositories
{
    public class CurrentWinrateEnemyRepository : ICreate<CurrentWinrateEnemy>, IRead<CurrentWinrateEnemy>, IUpdate<CurrentWinrateEnemy>, IResettable
    {
        protected readonly DotaStatDbContext _context;
        public CurrentWinrateEnemyRepository(DotaStatDbContext context)
        {
            _context = context;
        }
        public void Create(CurrentWinrateEnemy newEntity)
        {
            _context.CurrentWinrateEnemies.Add(newEntity);
        }

        public CurrentWinrateEnemy Read(params int[] id)
        {
            return _context.Find<CurrentWinrateEnemy>((byte)id[0], (byte)id[1]);
        }

        public IQueryable<CurrentWinrateEnemy> ReadAll()
        {
            return _context.Set<CurrentWinrateEnemy>();
        }

        public CurrentWinrateEnemy Update(CurrentWinrateEnemy newEnt)
        {
            return _context.CurrentWinrateEnemies.Update(newEnt).Entity;
        }
        public void ResetTable()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [CurrentWinrateEnemies]");
        }
    }
}
