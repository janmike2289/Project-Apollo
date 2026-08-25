using Apollo.Api.Endpoints;
using Apollo.Domain.Entities;
using Apollo.Infrastructure;
using Apollo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.EnsureSchemaAsync();

    if (!await db.ChangeTickets.AnyAsync())
    {
        db.ChangeTickets.AddRange(
            new ChangeTicket(
                "Deploy payment API v2",
                "Roll out payment API v2 behind the existing gateway with a 10% canary.",
                ChangeType.Normal,
                ChangePriority.High,
                "alex.nguyen",
                "sre-oncall",
                "Deploy canary, monitor error rate, promote to 100%.",
                "Revert gateway route to payment API v1.",
                DateTimeOffset.UtcNow.AddDays(1),
                DateTimeOffset.UtcNow.AddDays(1).AddHours(2)),
            new ChangeTicket(
                "Emergency firewall rule for vendor VPN",
                "Add a temporary allow rule for the vendor support VPN.",
                ChangeType.Emergency,
                ChangePriority.Critical,
                "priya.patel",
                "network-ops"));
        await db.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapChangeTicketEndpoints();

app.Run();
