using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ServiceDefaults;

namespace PatchChecker.Data;

public interface IDbConnectionFactory
{
    SqlConnection Create();
}

public class DbConnectionFactory : IDbConnectionFactory
{
    private string ConnectionString { get; set; }

    public DbConnectionFactory(IConfiguration configuration)
    {
        ConnectionString = configuration.GetRequiredConnectionString("DotaStat.Patch");
    }

    public SqlConnection Create()
    {
        return new SqlConnection(ConnectionString);
    }
}
