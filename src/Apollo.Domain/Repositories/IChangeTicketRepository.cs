using Apollo.Domain.Entities;
using Apollo.Domain.Specifications;

namespace Apollo.Domain.Repositories;

public interface IChangeTicketRepository
{
    Task<ChangeTicket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ChangeTicket>> ListAsync(
        ISpecification<ChangeTicket>? specification = null,
        CancellationToken cancellationToken = default);

    Task CreateAsync(ChangeTicket ticket, CancellationToken cancellationToken = default);
    Task UpdateAsync(ChangeTicket ticket, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<ChangeLogComment?> AddChangeLogCommentAsync(
        Guid ticketId,
        string body,
        string author,
        CancellationToken cancellationToken = default);

    Task<Attachment?> AttachEmailAsync(
        Guid ticketId,
        string fileName,
        string storageKey,
        string? emailFrom = null,
        string? emailSubject = null,
        string contentType = "message/rfc822",
        CancellationToken cancellationToken = default);

    Task<Attachment?> AttachScreenshotAsync(
        Guid ticketId,
        string fileName,
        string contentType,
        string storageKey,
        CancellationToken cancellationToken = default);
}
