namespace Apollo.Domain.Entities;

public enum ChangeType
{
    Standard = 0,
    Normal = 1,
    Emergency = 2
}

public enum ChangeStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Scheduled = 3,
    InProgress = 4,
    Completed = 5,
    Rejected = 6,
    Cancelled = 7
}

public enum ChangePriority
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}

public enum AttachmentKind
{
    Email = 0,
    Screenshot = 1
}
