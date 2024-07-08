using Dapper;
using PatchChecker.Data.Model;

namespace PatchChecker.Data.Repository;

public interface IDataPatchQueries
{
    Task<string> GetCurrentPatch();
    Task<IEnumerable<Patch>> GetPatches();
    Task<int> GetPatchesCount();
}

public class DataPatchQueries : IDataPatchQueries
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DataPatchQueries(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<string> GetCurrentPatch()
    {
        await using var dbConnection = _dbConnectionFactory.Create();

        return (await dbConnection.QueryFirstAsync<Patch>(
            """
            SELECT TOP 1 *
            FROM Patch
            ORDER BY Datetime DESC
            """
        )).Name;
    }

    public async Task<IEnumerable<Patch>> GetPatches()
    {
        await using var dbConnection = _dbConnectionFactory.Create();

        return await dbConnection.QueryAsync<Patch>(
            """
            SELECT COUNT(*)
            FROM Patch
            ORDER BY Datetime DESC
            """
        );
    }

    public async Task<int> GetPatchesCount()
    {
        await using var dbConnection = _dbConnectionFactory.Create();

        return await dbConnection.QueryFirstAsync<int>(
            """
            SELECT COUNT(*)
            FROM Patch
            ORDER BY Datetime DESC
            """
        );
    }
}
