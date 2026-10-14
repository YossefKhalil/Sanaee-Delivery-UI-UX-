using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
    {
        public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
        {
            builder.HasKey(h => h.HistoryId);

            builder.Property(h => h.Notes).HasMaxLength(500);

            builder.HasOne(h => h.Order)
                   .WithMany()
                   .HasForeignKey(h => h.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.User)
                   .WithMany()
                   .HasForeignKey(h => h.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h => h.Reason)
                   .WithMany()
                   .HasForeignKey(h => h.ReasonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}