using DB_Repositories.Interfaces;
using DotaStat.Data.EntityFramework.Model;
using Microsoft.EntityFrameworkCore;

namespace DB_Repositories
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
