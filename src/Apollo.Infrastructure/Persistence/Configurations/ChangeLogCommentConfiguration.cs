using Apollo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apollo.Infrastructure.Persistence.Configurations;

public sealed class ChangeLogCommentConfiguration : IEntityTypeConfiguration<ChangeLogComment>
{
    public void Configure(EntityTypeBuilder<ChangeLogComment> builder)
    {
        builder.ToTable("ChangeLogComments");
        builder.HasKey(comment => comment.Id);
        builder.Property(comment => comment.TicketId).IsRequired();
        builder.Property(comment => comment.Body).HasMaxLength(4000).IsRequired();
        builder.Property(comment => comment.Author).HasMaxLength(200).IsRequired();
        builder.Property(comment => comment.CreatedAt).IsRequired();
        builder.HasIndex(comment => comment.TicketId);
    }
}
