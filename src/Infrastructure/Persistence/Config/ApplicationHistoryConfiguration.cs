using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class ApplicationHistoryConfiguration : IEntityTypeConfiguration<ApplicationHistory>
    {
        public void Configure(EntityTypeBuilder<ApplicationHistory> builder)
        {
            builder.HasKey(a => a.HistoryId);

            builder.Property(a => a.Notes).HasMaxLength(500);

            builder.HasOne(a => a.Provider)
                   .WithMany(p => p.ApplicationHistories)
                   .HasForeignKey(a => a.ProviderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.User)
                   .WithMany()
                   .HasForeignKey(a => a.ChangedBy)
                   .OnDelete(DeleteBehavior.Restrict); // لمنع الـ Cascade Delete
        }
    }
}
