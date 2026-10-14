
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(al => al.LogId);

            builder.Property(al => al.Reason).HasMaxLength(500);

            builder.HasOne(al => al.PerformedBy)
                   .WithMany()
                   .HasForeignKey(al => al.PerformedById)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
