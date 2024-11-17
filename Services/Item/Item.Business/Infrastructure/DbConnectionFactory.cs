using Microsoft.Data.SqlClient;

namespace Item.Business.Infrastructure;

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
