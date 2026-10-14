using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.WalletId);

            builder.Property(w => w.Balance)
                .HasColumnType("decimal(18,2)");

            builder.Property(w => w.RowVersion)
               .IsRowVersion();

            builder.HasIndex(w => w.UserId)
                .IsUnique();

            builder.HasQueryFilter(w => w.IsActive);
        }
    }
}
