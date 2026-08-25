using Apollo.Domain.Entities;

namespace Apollo.Domain.Specifications;

public sealed class ChangeTicketsQuerySpec : Specification<ChangeTicket>
{
    public ChangeTicketsQuerySpec(
        string? title = null,
        ChangeStatus? status = null,
        ChangeType? changeType = null,
        string? requester = null,
        int skip = 0,
        int take = 50)
    {
        Query(ticket =>
            (string.IsNullOrWhiteSpace(title) || ticket.Title.Contains(title)) &&
            (!status.HasValue || ticket.Status == status.Value) &&
            (!changeType.HasValue || ticket.ChangeType == changeType.Value) &&
            (string.IsNullOrWhiteSpace(requester) || ticket.Requester.Contains(requester)));

        ApplyOrderByDescending(ticket => ticket.UpdatedAt);
        ApplyPaging(skip, take);
    }
}
