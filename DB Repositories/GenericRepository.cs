using System.Linq;
using DataBase;
using DB_Repositories.Interfaces;
using DotaStat.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace DB_Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly DotaStatDbContext _context;
        public GenericRepository(DbContext context)
        {
            _context = (DotaStatDbContext)context;
        }
        public void Create(T newEnt)
        {
            _context.Add(newEnt);
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
