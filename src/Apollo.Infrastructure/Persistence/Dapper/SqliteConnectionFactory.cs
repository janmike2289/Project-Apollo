using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Apollo.Infrastructure.Persistence.Dapper;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}

public sealed class SqliteConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection() =>
        new SqliteConnection(configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' was not found."));
}
