using System.Linq;
using DotaStat.Data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotaStat.Data.EntityFramework.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly DotaStatDbContext _context;
        public GenericRepository(DbContext context)
        {
            _context = (DotaStatDbContext)context;
        }
        public void Create(T newEntity)
        {
            _context.Add(newEntity);
        }
        public T Read(params int[] id)
        {
            return _context.Find<T>(id);
        }
        public IQueryable<T> ReadAll()
        {
            return _context.Set<T>();
        }
        public T Update(T newEnt)
        {
            return _context.Update(newEnt).Entity;
        }
        public virtual void Delete(T ent)
        {
            _context.Remove(ent);
        }
    }
}
