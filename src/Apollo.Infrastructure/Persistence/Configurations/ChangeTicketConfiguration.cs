using Apollo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apollo.Infrastructure.Persistence.Configurations;

public sealed class ChangeTicketConfiguration : IEntityTypeConfiguration<ChangeTicket>
{
    public void Configure(EntityTypeBuilder<ChangeTicket> builder)
    {
        builder.ToTable("ChangeTickets");
        builder.HasKey(ticket => ticket.Id);
        builder.Property(ticket => ticket.Title).HasMaxLength(200).IsRequired();
        builder.Property(ticket => ticket.Description).HasMaxLength(8000).IsRequired();
        builder.Property(ticket => ticket.ChangeType).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(ticket => ticket.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(ticket => ticket.Priority).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(ticket => ticket.Requester).HasMaxLength(200).IsRequired();
        builder.Property(ticket => ticket.AssignedTo).HasMaxLength(200);
        builder.Property(ticket => ticket.ImplementationPlan).HasMaxLength(8000);
        builder.Property(ticket => ticket.RollbackPlan).HasMaxLength(8000);
        builder.Ignore(ticket => ticket.ChangeLog);
        builder.Ignore(ticket => ticket.Attachments);
    }
}
