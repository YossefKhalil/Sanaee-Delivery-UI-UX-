using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.LaborCost).HasColumnType("decimal(18,2)");
            builder.Property(o => o.MaterialCost).HasColumnType("decimal(18,2)");
            builder.Property(o => o.Notes).HasMaxLength(500);

            builder.HasOne(o => o.Order)
                   .WithMany(ord => ord.Offers)
                   .HasForeignKey(o => o.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Provider)
                   .WithMany()
                   .HasForeignKey(o => o.ProviderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter
                (
                    o => 
                        o.ExpiresAt < DateTime.UtcNow ||
                        o.Status != OfferStatus.Cancelled ||
                        o.Status != OfferStatus.Rejected ||
                        o.Status != OfferStatus.Expired
                );
        }
    }
}