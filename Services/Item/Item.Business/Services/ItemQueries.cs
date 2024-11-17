using Dapper;
using Item.Business.Infrastructure;

namespace Item.Business.Services;

public interface IItemQueries
{
    Task<IEnumerable<Types.Item>> GetAllAsync();
}

public class ItemQueries(IDbConnectionFactory dbConnectionFactory) : IItemQueries
{
    public async Task<IEnumerable<Types.Item>> GetAllAsync()
    {
        await using var dbConnection = dbConnectionFactory.Create();

        return await dbConnection.QueryAsync<Types.Item>(
            """
            SELECT *
            FROM Item
            """
        );
    }
}
