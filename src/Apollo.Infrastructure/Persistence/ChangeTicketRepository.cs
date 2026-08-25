using Apollo.Domain.Entities;
using Apollo.Domain.Repositories;
using Apollo.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Infrastructure.Persistence;

public sealed class ChangeTicketRepository(AppDbContext dbContext) : IChangeTicketRepository
{
    public async Task<ChangeTicket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var ticket = await dbContext.ChangeTickets.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (ticket is not null)
        {
            await HydrateAsync([ticket], cancellationToken);
        }

        return ticket;
    }

    public async Task<IReadOnlyList<ChangeTicket>> ListAsync(
        ISpecification<ChangeTicket>? specification = null,
        CancellationToken cancellationToken = default)
    {
        var tickets = await SpecificationEvaluator.GetQuery(dbContext.ChangeTickets, specification)
            .ToListAsync(cancellationToken);

        await HydrateAsync(tickets, cancellationToken);
        return tickets;
    }

    public async Task CreateAsync(ChangeTicket ticket, CancellationToken cancellationToken = default)
    {
        await dbContext.ChangeTickets.AddAsync(ticket, cancellationToken);
        await PersistChildrenAsync(ticket, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ChangeTicket ticket, CancellationToken cancellationToken = default)
    {
        var entry = dbContext.Entry(ticket);
        if (entry.State == EntityState.Detached)
        {
            dbContext.ChangeTickets.Update(ticket);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var ticket = await dbContext.ChangeTickets.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (ticket is null)
        {
            return false;
        }

        await dbContext.ChangeLogComments.Where(comment => comment.TicketId == id)
            .ExecuteDeleteAsync(cancellationToken);
        await dbContext.Attachments.Where(attachment => attachment.TicketId == id)
            .ExecuteDeleteAsync(cancellationToken);

        dbContext.ChangeTickets.Remove(ticket);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ChangeLogComment?> AddChangeLogCommentAsync(
        Guid ticketId,
        string body,
        string author,
        CancellationToken cancellationToken = default)
    {
        var ticket = await dbContext.ChangeTickets.FirstOrDefaultAsync(item => item.Id == ticketId, cancellationToken);
        if (ticket is null)
        {
            return null;
        }

        var entry = ticket.AddChangeLogComment(body, author);
        await dbContext.ChangeLogComments.AddAsync(entry, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entry;
    }

    public async Task<Attachment?> AttachEmailAsync(
        Guid ticketId,
        string fileName,
        string storageKey,
        string? emailFrom = null,
        string? emailSubject = null,
        string contentType = "message/rfc822",
        CancellationToken cancellationToken = default)
    {
        var ticket = await dbContext.ChangeTickets.FirstOrDefaultAsync(item => item.Id == ticketId, cancellationToken);
        if (ticket is null)
        {
            return null;
        }

        var attachment = ticket.AttachEmail(fileName, storageKey, emailFrom, emailSubject, contentType);
        await dbContext.Attachments.AddAsync(attachment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return attachment;
    }

    public async Task<Attachment?> AttachScreenshotAsync(
        Guid ticketId,
        string fileName,
        string contentType,
        string storageKey,
        CancellationToken cancellationToken = default)
    {
        var ticket = await dbContext.ChangeTickets.FirstOrDefaultAsync(item => item.Id == ticketId, cancellationToken);
        if (ticket is null)
        {
            return null;
        }

        var attachment = ticket.AttachScreenshot(fileName, contentType, storageKey);
        await dbContext.Attachments.AddAsync(attachment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return attachment;
    }

    private async Task PersistChildrenAsync(ChangeTicket ticket, CancellationToken cancellationToken)
    {
        foreach (var entry in ticket.ChangeLog)
        {
            await dbContext.ChangeLogComments.AddAsync(entry, cancellationToken);
        }

        foreach (var attachment in ticket.Attachments)
        {
            await dbContext.Attachments.AddAsync(attachment, cancellationToken);
        }
    }

    private async Task HydrateAsync(IReadOnlyCollection<ChangeTicket> tickets, CancellationToken cancellationToken)
    {
        if (tickets.Count == 0)
        {
            return;
        }

        var ids = tickets.Select(ticket => ticket.Id).ToArray();

        var changeLog = await dbContext.ChangeLogComments
            .AsNoTracking()
            .Where(comment => ids.Contains(comment.TicketId))
            .OrderBy(comment => comment.CreatedAt)
            .ToListAsync(cancellationToken);

        var attachments = await dbContext.Attachments
            .AsNoTracking()
            .Where(attachment => ids.Contains(attachment.TicketId))
            .OrderBy(attachment => attachment.CreatedAt)
            .ToListAsync(cancellationToken);

        foreach (var ticket in tickets)
        {
            ticket.SetChangeLog(changeLog.Where(comment => comment.TicketId == ticket.Id));
            ticket.SetAttachments(attachments.Where(attachment => attachment.TicketId == ticket.Id));
        }
    }
}
