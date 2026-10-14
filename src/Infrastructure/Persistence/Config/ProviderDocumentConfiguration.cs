using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class ProviderDocumentConfiguration : IEntityTypeConfiguration<ProviderDocument>
    {
        public void Configure(EntityTypeBuilder<ProviderDocument> builder)
        {
            builder.HasKey(d => d.DocumentId);

            builder.Property(d => d.DocumentUrl).HasMaxLength(500).IsRequired();

            builder.HasOne(d => d.Provider)
                   .WithMany(p => p.ProviderDocuments)
                   .HasForeignKey(d => d.ProviderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.User)
                   .WithMany()
                   .HasForeignKey(h => h.ChangedBy)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}