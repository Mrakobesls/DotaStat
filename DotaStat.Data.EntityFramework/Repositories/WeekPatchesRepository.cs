using System.Linq;
using DotaStat.Data.EntityFramework.Model;
using DotaStat.Data.EntityFramework.Repositories;

namespace DotaStat.Data.EntityFramework.Repositories
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
            return ReadAll().First(x => x.Id == id[0]);
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
