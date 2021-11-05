using System.Linq;

namespace DotaStat.Data.EntityFramework.Repositories
{
    interface IRead<T>
        where T : class
    {
        T Read(params int[] id);
        IQueryable<T> ReadAll();
    }
}
