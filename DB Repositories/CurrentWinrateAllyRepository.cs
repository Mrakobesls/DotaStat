using System.Linq;
using DataBase;
using DataBase.Model;
using DB_Repositories.Interfaces;
using DotaStat.Data.EntityFramework;
using DotaStat.Data.EntityFramework.Model;
using Microsoft.EntityFrameworkCore;

namespace DB_Repositories
{
    public class CurrentWinrateAllyRepository : IRead<CurrentWinrateAlly>, IUpdate<CurrentWinrateAlly>
    {
        protected readonly DotaStatDbContext _context;
        public CurrentWinrateAllyRepository(DotaStatDbContext context)
        {
            _context = context;
        }

        public CurrentWinrateAlly Read(params int[] id)
        {
            return _context.Find<CurrentWinrateAlly>(id);
        }
        public IQueryable<CurrentWinrateAlly> ReadAll()
        {
            return _context.Set<CurrentWinrateAlly>();
        }
        public CurrentWinrateAlly Update(CurrentWinrateAlly newEnt)
        {
            return _context.Update(newEnt).Entity;
        }
    }
}
