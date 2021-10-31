using System.Linq;

namespace DB_Repositories.Interfaces
{
    interface IGenericRepository<T> : ICreate<T>, IRead<T>, IUpdate<T>, IDelete<T>
        where T : class
    {

    }
}
