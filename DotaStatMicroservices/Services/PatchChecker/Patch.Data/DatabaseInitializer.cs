using DbUp;

namespace PatchChecker.Data;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;

        InitializeAsync();
    }

    private void InitializeAsync()
    {
        EnsureDatabase.For.SqlDatabase(_connectionString);

        var upgrader = DeployChanges.To.SqlDatabase(_connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(DatabaseInitializer).Assembly)
            .LogToConsole()
            .Build();

        if (upgrader.IsUpgradeRequired())
        {
            upgrader.PerformUpgrade();
        }
    }
}
