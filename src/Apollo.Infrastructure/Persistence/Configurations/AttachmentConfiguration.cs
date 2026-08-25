using Apollo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apollo.Infrastructure.Persistence.Configurations;

public sealed class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("ChangeAttachments");
        builder.HasKey(attachment => attachment.Id);
        builder.Property(attachment => attachment.TicketId).IsRequired();
        builder.Property(attachment => attachment.Kind).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(attachment => attachment.FileName).HasMaxLength(260).IsRequired();
        builder.Property(attachment => attachment.ContentType).HasMaxLength(200).IsRequired();
        builder.Property(attachment => attachment.StorageKey).HasMaxLength(1000).IsRequired();
        builder.Property(attachment => attachment.EmailFrom).HasMaxLength(320);
        builder.Property(attachment => attachment.EmailSubject).HasMaxLength(500);
        builder.Property(attachment => attachment.CreatedAt).IsRequired();
        builder.HasIndex(attachment => attachment.TicketId);
    }
}
