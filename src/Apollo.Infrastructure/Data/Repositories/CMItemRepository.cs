using System.Data;
using Apollo.Domain.Entities;
using Apollo.Domain.Interface;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Apollo.Infrastructure.Persistence.Repositories;

public class CMItemRepository(IConfiguration configuration) : ICMItemRepository
{
    private readonly string connectionString = configuration.GetConnectionString("DevConnection")
            ?? throw new ArgumentNullException(nameof(configuration), "Connection string not found.");

    private IDbConnection CreateConnection() => new SqlConnection(connectionString);

    public async Task<IEnumerable<CMItemEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        //const string sql = "select Name, ItemType, Status, Priority, Publish, RequestedBy, Module, TargetDate, Description, PublishStatement, Objects, Createdby, CreatedOn, ChangedBy, ChangedOn, AssignedTo from CMItem";
        
        const string sql = "select Name from CMItem";

        using var connection = CreateConnection();
        
        var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);
        return await connection.QueryAsync<CMItemEntity>(cmd);
    }

    public async Task<CMItemEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = "select Name from CMItem WHERE itemid = @Id";
        
        using var connection = CreateConnection();
        var cmd = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
        return await connection.QueryFirstOrDefaultAsync<CMItemEntity>(cmd);
    }

    public async Task<int> CreateAsync(CMItemEntity product, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO Products (Name, Price, Stock) 
            VALUES (@Name, @Price, @Stock);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        using var connection = CreateConnection();
        var cmd = new CommandDefinition(sql, product, cancellationToken: cancellationToken);
        return await connection.ExecuteScalarAsync<int>(cmd);
    }

    public async Task<bool> UpdateAsync(CMItemEntity product, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE Products 
            SET Name = @Name, Price = @Price, Stock = @Stock 
            WHERE Id = @Id";

        using var connection = CreateConnection();
        var cmd = new CommandDefinition(sql, product, cancellationToken: cancellationToken);
        int rowsAffected = await connection.ExecuteAsync(cmd);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM Products WHERE Id = @Id";

        using var connection = CreateConnection();
        var cmd = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
        int rowsAffected = await connection.ExecuteAsync(cmd);
        return rowsAffected > 0;
    }

    public Task<CMItemEntity> AddAsync(CMItemEntity changeManagement, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    Task ICMItemRepository.UpdateAsync(CMItemEntity changeManagement, CancellationToken cancellationToken)
    {
        return UpdateAsync(changeManagement, cancellationToken);
    }
}