using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ServiceDefaults;

namespace Patch.Data;

public interface IDbConnectionFactory
{
    SqlConnection Create();
}

public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public SqlConnection Create()
    {
        return new SqlConnection(connectionString);
    }
}
