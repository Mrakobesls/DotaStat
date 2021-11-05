namespace DotaStat.Data.EntityFramework.Repositories
{
    interface ICreate<T>
        where T : class
    {
        void Create(T newEntity);
    }
}
