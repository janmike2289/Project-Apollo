namespace Apollo.Domain.Entities;

public class ChangeLogComment : Entity
{
    public Guid TicketId { get; private set; }
    public string Body { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }

    private ChangeLogComment()
    {
    }

    public ChangeLogComment(Guid ticketId, string body, string author)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(ticketId, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(body);
        ArgumentException.ThrowIfNullOrWhiteSpace(author);

        Id = Guid.NewGuid();
        TicketId = ticketId;
        Body = body.Trim();
        Author = author.Trim();
        CreatedAt = DateTimeOffset.UtcNow;
    }
}
