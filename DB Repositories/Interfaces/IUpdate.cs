namespace DB_Repositories.Interfaces
{
    interface IUpdate<T>
        where T : class
    {
        T Update(T newUser);
    }
}
