using Microsoft.EntityFrameworkCore;

namespace CommonExtensions;

public static class DbSetExtensions
{
    public static void Truncate<T>(this DbSet<T> dbSet)
        where T : class
    {
        dbSet.RemoveRange(dbSet);
    }

    public static async Task<IdentityInsert<T>> StartIdentityInsert<T>(this DbContext context)
        where T : class
    {
        var insert = new IdentityInsert<T>(context);
        await insert.StartInsert();

        return insert;
    }
}

public class IdentityInsert<T> : IAsyncDisposable
    where T : class
{
    private readonly DbContext _dbContext;

    public IdentityInsert(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task StartInsert()
    {
        await EnableIdentityInsert();
    }

    public async Task FinishInsert()
    {
        await DisableIdentityInsert();
    }

    public async ValueTask DisposeAsync()
    {
        await FinishInsert();
    }

    private Task EnableIdentityInsert()
    {
        return SetIdentityInsert(true);
    }

    private Task DisableIdentityInsert()
    {
        return SetIdentityInsert(false);
    }

    private async Task SaveChangesWithIdentityInsert()
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        await EnableIdentityInsert();
        await _dbContext.SaveChangesAsync();
        await DisableIdentityInsert();
        await transaction.CommitAsync();
    }

    private async Task SetIdentityInsert(bool enable)
    {
        var entityType = _dbContext.Model.FindEntityType(typeof(T));
        var val = enable ? "ON" : "OFF";
        var schema = entityType.GetSchema() ?? "dbo";
        var tableName = entityType.GetTableName();
        await _dbContext.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT [{schema}].[{tableName}] {val}");
    }
}
