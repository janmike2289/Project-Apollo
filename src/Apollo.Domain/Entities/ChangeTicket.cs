namespace Apollo.Domain.Entities;

public class ChangeTicket : AggregateRoot
{
    private readonly List<ChangeLogComment> _changeLog = [];
    private readonly List<Attachment> _attachments = [];

    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ChangeType ChangeType { get; private set; }
    public ChangeStatus Status { get; private set; }
    public ChangePriority Priority { get; private set; }
    public string Requester { get; private set; } = string.Empty;
    public string? AssignedTo { get; private set; }
    public string? ImplementationPlan { get; private set; }
    public string? RollbackPlan { get; private set; }
    public DateTimeOffset? ScheduledStart { get; private set; }
    public DateTimeOffset? ScheduledEnd { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public IReadOnlyCollection<ChangeLogComment> ChangeLog => _changeLog.AsReadOnly();
    public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();

    private ChangeTicket()
    {
    }

    public ChangeTicket(
        string title,
        string description,
        ChangeType changeType,
        ChangePriority priority,
        string requester,
        string? assignedTo = null,
        string? implementationPlan = null,
        string? rollbackPlan = null,
        DateTimeOffset? scheduledStart = null,
        DateTimeOffset? scheduledEnd = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(requester);

        Id = Guid.NewGuid();
        Title = title.Trim();
        Description = description.Trim();
        ChangeType = changeType;
        Priority = priority;
        Requester = requester.Trim();
        AssignedTo = Normalize(assignedTo);
        ImplementationPlan = Normalize(implementationPlan);
        RollbackPlan = Normalize(rollbackPlan);
        ScheduledStart = scheduledStart;
        ScheduledEnd = scheduledEnd;
        Status = ChangeStatus.Draft;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void Update(
        string title,
        string description,
        ChangeType changeType,
        ChangeStatus status,
        ChangePriority priority,
        string requester,
        string? assignedTo,
        string? implementationPlan,
        string? rollbackPlan,
        DateTimeOffset? scheduledStart,
        DateTimeOffset? scheduledEnd)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(requester);

        Title = title.Trim();
        Description = description.Trim();
        ChangeType = changeType;
        Status = status;
        Priority = priority;
        Requester = requester.Trim();
        AssignedTo = Normalize(assignedTo);
        ImplementationPlan = Normalize(implementationPlan);
        RollbackPlan = Normalize(rollbackPlan);
        ScheduledStart = scheduledStart;
        ScheduledEnd = scheduledEnd;
        Touch();
    }

    public ChangeLogComment AddChangeLogComment(string body, string author)
    {
        var entry = new ChangeLogComment(Id, body, author);
        _changeLog.Add(entry);
        Touch();
        return entry;
    }

    public Attachment AttachEmail(
        string fileName,
        string storageKey,
        string? emailFrom = null,
        string? emailSubject = null,
        string contentType = "message/rfc822")
    {
        var attachment = Attachment.Email(Id, fileName, storageKey, emailFrom, emailSubject, contentType);
        _attachments.Add(attachment);
        Touch();
        return attachment;
    }

    public Attachment AttachScreenshot(string fileName, string contentType, string storageKey)
    {
        var attachment = Attachment.Screenshot(Id, fileName, contentType, storageKey);
        _attachments.Add(attachment);
        Touch();
        return attachment;
    }

    internal void SetChangeLog(IEnumerable<ChangeLogComment> entries)
    {
        _changeLog.Clear();
        _changeLog.AddRange(entries);
    }

    internal void SetAttachments(IEnumerable<Attachment> attachments)
    {
        _attachments.Clear();
        _attachments.AddRange(attachments);
    }

    private void Touch() => UpdatedAt = DateTimeOffset.UtcNow;

    private static string? Normalize(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
