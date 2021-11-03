using DB_Repositories.Interfaces;
using DotaStat.Data.EntityFramework;
using DotaStat.Data.EntityFramework.Model;
using System.Linq;

namespace DB_Repositories
{
    public class WeekPatchesRepository : IWeekPatchesRepository
    {
        protected readonly DotaStatDbContext _context;
        public WeekPatchesRepository(DotaStatDbContext context)
        {
            _context = context;
        }

        public WeekPatch Read(params int[] id)
        {
            return ReadAll().First(x => x.WeekId == id[0]);
        }

        public IQueryable<WeekPatch> ReadAll()
        {
            return _context.Set<WeekPatch>();
        }

        public WeekPatch Update(WeekPatch newEnt)
        {
            return _context.Update(newEnt).Entity;
        }

        public void Create(WeekPatch newEntity)
        {
            _context.Add(newEntity);
        }
    }
}
