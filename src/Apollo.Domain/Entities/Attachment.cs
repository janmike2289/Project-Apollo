namespace Apollo.Domain.Entities;

public class Attachment : Entity
{
    public Guid TicketId { get; private set; }
    public AttachmentKind Kind { get; private set; }
    public string FileName { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public string StorageKey { get; private set; } = string.Empty;
    public string? EmailFrom { get; private set; }
    public string? EmailSubject { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private Attachment()
    {
    }

    public static Attachment Email(
        Guid ticketId,
        string fileName,
        string storageKey,
        string? emailFrom = null,
        string? emailSubject = null,
        string contentType = "message/rfc822")
    {
        ArgumentOutOfRangeException.ThrowIfEqual(ticketId, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(storageKey);
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);

        return new Attachment
        {
            Id = Guid.NewGuid(),
            TicketId = ticketId,
            Kind = AttachmentKind.Email,
            FileName = fileName.Trim(),
            ContentType = contentType.Trim(),
            StorageKey = storageKey.Trim(),
            EmailFrom = string.IsNullOrWhiteSpace(emailFrom) ? null : emailFrom.Trim(),
            EmailSubject = string.IsNullOrWhiteSpace(emailSubject) ? null : emailSubject.Trim(),
            CreatedAt = DateTimeOffset.UtcNow
        };
    }

    public static Attachment Screenshot(
        Guid ticketId,
        string fileName,
        string contentType,
        string storageKey)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(ticketId, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(contentType);
        ArgumentException.ThrowIfNullOrWhiteSpace(storageKey);

        if (!contentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Screenshot attachments must use an image content type.", nameof(contentType));
        }

        return new Attachment
        {
            Id = Guid.NewGuid(),
            TicketId = ticketId,
            Kind = AttachmentKind.Screenshot,
            FileName = fileName.Trim(),
            ContentType = contentType.Trim(),
            StorageKey = storageKey.Trim(),
            CreatedAt = DateTimeOffset.UtcNow
        };
    }
}
