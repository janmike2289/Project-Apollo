using Apollo.Domain.Entities;

namespace Apollo.Domain.Specifications;

public sealed class ChangeTicketByIdSpec : Specification<ChangeTicket>
{
    public ChangeTicketByIdSpec(Guid id)
    {
        Query(ticket => ticket.Id == id);
        EnableTracking();
    }
}
