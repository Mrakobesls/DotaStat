using Dapper;
using Patch.Data.Types;

namespace Patch.Data.Repository;

public interface IDataPatchCommands
{
    Task AddNewPatch(PatchCreate patch);
    Task AddPatchHistory(IEnumerable<PatchCreate> patches);
}

public class DataPatchCommands : IDataPatchCommands
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public DataPatchCommands(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task AddNewPatch(PatchCreate patch)
    {
        await using var dbConnection = _dbConnectionFactory.Create();

        await dbConnection.ExecuteAsync(
            """
            -- noinspection SqlResolveForFile @ variable/"@Name"
            -- noinspection SqlResolveForFile @ variable/"@DateTime"
            INSERT INTO Patch (Name, DateTime)
            VALUES (@Name, @DateTime)
            """,
            patch
        );
    }

    public async Task AddPatchHistory(IEnumerable<PatchCreate> patches)
    {
        await using var dbConnection = _dbConnectionFactory.Create();

        await dbConnection.ExecuteAsync(
            """
            -- noinspection SqlResolveForFile @ variable/"@Name"
            -- noinspection SqlResolveForFile @ variable/"@DateTime"
            INSERT INTO Patch (Name, DateTime)
            VALUES (@Name, @DateTime)
            """,
            patches
        );
    }
}
