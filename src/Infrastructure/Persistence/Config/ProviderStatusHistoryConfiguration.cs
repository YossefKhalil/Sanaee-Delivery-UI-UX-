using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class ProviderStatusHistoryConfiguration : IEntityTypeConfiguration<ProviderStatusHistory>
    {
        public void Configure(EntityTypeBuilder<ProviderStatusHistory> builder)
        {
            builder.HasKey(h => h.HistoryId);

            builder.Property(h => h.Notes).HasMaxLength(500);

            builder.HasOne(h => h.Provider)
                   .WithMany()
                   .HasForeignKey(h => h.ProviderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Admin)
                   .WithMany()
                   .HasForeignKey(h => h.ChangedBy)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}