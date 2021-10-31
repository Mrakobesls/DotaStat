using System.Linq;

namespace DB_Repositories.Interfaces
{
    interface IRead<T>
        where T : class
    {
        T Read(params int[] id);
        IQueryable<T> ReadAll();
    }
}
