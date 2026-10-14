using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProviderCategoryConfiguration : IEntityTypeConfiguration<ProviderCategory>
    {
        public void Configure(EntityTypeBuilder<ProviderCategory> builder)
        {
            builder.HasKey(pc => pc.ProviderCategoryId);

            // منع تكرار نفس القسم لنفس الفني
            builder.HasIndex(pc => new { pc.ProviderId, pc.CategoryId }).IsUnique();

            builder.HasOne(pc => pc.Provider)
                   .WithMany(p => p.ProviderCategories)
                   .HasForeignKey(pc => pc.ProviderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pc => pc.Category)
                   .WithMany()
                   .HasForeignKey(pc => pc.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
