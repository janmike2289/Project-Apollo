using Apollo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ChangeTicket> ChangeTickets => Set<ChangeTicket>();
    public DbSet<ChangeLogComment> ChangeLogComments => Set<ChangeLogComment>();
    public DbSet<Attachment> Attachments => Set<Attachment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public async Task EnsureSchemaAsync(CancellationToken cancellationToken = default)
    {
        await Database.EnsureCreatedAsync(cancellationToken);

        await Database.ExecuteSqlRawAsync(
            """
            CREATE TABLE IF NOT EXISTS "ChangeTickets" (
                "Id" TEXT NOT NULL CONSTRAINT "PK_ChangeTickets" PRIMARY KEY,
                "Title" TEXT NOT NULL,
                "Description" TEXT NOT NULL,
                "ChangeType" TEXT NOT NULL,
                "Status" TEXT NOT NULL,
                "Priority" TEXT NOT NULL,
                "Requester" TEXT NOT NULL,
                "AssignedTo" TEXT NULL,
                "ImplementationPlan" TEXT NULL,
                "RollbackPlan" TEXT NULL,
                "ScheduledStart" TEXT NULL,
                "ScheduledEnd" TEXT NULL,
                "CreatedAt" TEXT NOT NULL,
                "UpdatedAt" TEXT NOT NULL
            );
            """,
            cancellationToken);

        await Database.ExecuteSqlRawAsync(
            """
            CREATE TABLE IF NOT EXISTS "ChangeLogComments" (
                "Id" TEXT NOT NULL CONSTRAINT "PK_ChangeLogComments" PRIMARY KEY,
                "TicketId" TEXT NOT NULL,
                "Body" TEXT NOT NULL,
                "Author" TEXT NOT NULL,
                "CreatedAt" TEXT NOT NULL
            );
            """,
            cancellationToken);

        await Database.ExecuteSqlRawAsync(
            """
            CREATE TABLE IF NOT EXISTS "ChangeAttachments" (
                "Id" TEXT NOT NULL CONSTRAINT "PK_ChangeAttachments" PRIMARY KEY,
                "TicketId" TEXT NOT NULL,
                "Kind" TEXT NOT NULL,
                "FileName" TEXT NOT NULL,
                "ContentType" TEXT NOT NULL,
                "StorageKey" TEXT NOT NULL,
                "EmailFrom" TEXT NULL,
                "EmailSubject" TEXT NULL,
                "CreatedAt" TEXT NOT NULL
            );
            """,
            cancellationToken);
    }
}
