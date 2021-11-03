using DB_Repositories.Interfaces;
using DotaStat.Data.EntityFramework;
using DotaStat.Data.EntityFramework.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DB_Repositories
{
    public class CurrentWinrateAllyRepository : ICreate<CurrentWinrateAlly>, IRead<CurrentWinrateAlly>, IUpdate<CurrentWinrateAlly>, IResettable
    {
        protected readonly DotaStatDbContext _context;
        public CurrentWinrateAllyRepository(DotaStatDbContext context)
        {
            _context = context;
        }

        public void Create(CurrentWinrateAlly newEntity)
        {
            _context.CurrentWinrateAllies.Add(newEntity);
        }

        public CurrentWinrateAlly Read(params int[] id)
        {
            return _context.Find<CurrentWinrateAlly>((byte)id[0], (byte)id[1]);
        }
        public IQueryable<CurrentWinrateAlly> ReadAll()
        {
            return _context.Set<CurrentWinrateAlly>();
        }
        public CurrentWinrateAlly Update(CurrentWinrateAlly newEnt)
        {
            return _context.CurrentWinrateAllies.Update(newEnt).Entity;
        }
        public void ResetTable()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [CurrentWinrateAllies]");
        }
    }
}
