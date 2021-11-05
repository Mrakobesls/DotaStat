namespace DotaStat.Data.EntityFramework.Repositories
{
    interface IGenericRepository<T> : ICreate<T>, IRead<T>, IUpdate<T>, IDelete<T>
        where T : class
    {

    }
}
