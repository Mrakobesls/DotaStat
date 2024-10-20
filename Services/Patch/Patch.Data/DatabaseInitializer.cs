using DbUp;

namespace Patch.Data;

public class DatabaseInitializer(string connectionString)
{
    public void Initialize()
    {
        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = DeployChanges.To.SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(DatabaseInitializer).Assembly)
            .LogToConsole()
            .Build();

        if (upgrader.IsUpgradeRequired())
        {
            upgrader.PerformUpgrade();
        }
    }
}
