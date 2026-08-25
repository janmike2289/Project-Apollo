using Apollo.Domain.Repositories;
using Apollo.Infrastructure.Persistence;
using Apollo.Infrastructure.Persistence.Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apollo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' was not found.");

        DapperTypeHandlers.Register();

        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IChangeTicketRepository, ChangeTicketRepository>();
        services.AddSingleton<ISqlConnectionFactory, SqliteConnectionFactory>();
        services.AddScoped<IDapperQuery, DapperQuery>();

        return services;
    }
}
