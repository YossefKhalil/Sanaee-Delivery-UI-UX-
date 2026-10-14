using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Config
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.ProviderId);

            builder.HasIndex(p => p.NationalNumber).IsUnique();
            builder.Property(p => p.NationalNumber).HasMaxLength(14).IsRequired();

        }
    }
}
