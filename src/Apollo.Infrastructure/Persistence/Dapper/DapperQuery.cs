using System.Data;
using Apollo.Domain.Repositories;
using Dapper;

namespace Apollo.Infrastructure.Persistence.Dapper;

public sealed class DapperQuery(ISqlConnectionFactory connectionFactory) : IDapperQuery
{
    public async Task<IReadOnlyList<T>> QueryAsync<T>(
        string sql,
        object? parameters = null,
        CancellationToken cancellationToken = default)
    {
        using var connection = OpenConnection();
        var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);
        var result = await connection.QueryAsync<T>(command);
        return result.AsList();
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T>(
        string sql,
        object? parameters = null,
        CancellationToken cancellationToken = default)
    {
        using var connection = OpenConnection();
        var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync<T>(command);
    }

    public async Task<int> ExecuteAsync(
        string sql,
        object? parameters = null,
        CancellationToken cancellationToken = default)
    {
        using var connection = OpenConnection();
        var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);
        return await connection.ExecuteAsync(command);
    }

    private IDbConnection OpenConnection()
    {
        var connection = connectionFactory.CreateConnection();
        connection.Open();
        return connection;
    }
}
