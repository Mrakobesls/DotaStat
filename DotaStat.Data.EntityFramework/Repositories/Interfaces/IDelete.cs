namespace DotaStat.Data.EntityFramework.Repositories
{
    interface IDelete<T>
        where T : class
    {
        void Delete(T ent);
    }
}
