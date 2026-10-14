using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            builder.HasKey(wt => wt.TransactionId);

            builder.Property(wt => wt.Amount).HasColumnType("decimal(18,2)");
            builder.Property(wt => wt.Description).HasMaxLength(255);

            builder.HasOne(wt => wt.Wallet)
                   .WithMany(w => w.WalletTransactions)
                   .HasForeignKey(wt => wt.WalletId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wt => wt.Order)
                   .WithMany()
                   .HasForeignKey(wt => wt.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}