namespace DotaStat.Data.EntityFramework.Repositories
{
    interface IUpdate<T>
        where T : class
    {
        T Update(T newUser);
    }
}
