using DotaStat.Data.EntityFramework.Model;
using DotaStat.Data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotaStat.Data.EntityFramework.Repositories
{
    public class HeroRepository : GenericRepository<Hero>, IResettable
    {
        public HeroRepository(DbContext context) : base(context)
        {
        }

        public void ResetTable()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Heroes]");
        }
    }
}
