using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.ReviewId);

            builder.Property(r => r.Comment).HasMaxLength(1000);
            builder.HasIndex(r => new { r.OrderId, r.ReviewerId }).IsUnique();

            // Reviewer (User)
            builder.HasOne(r => r.Reviewer)
                   .WithMany()
                   .HasForeignKey(r => r.ReviewerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Reviewee (User)
            builder.HasOne(r => r.Reviewee)
                   .WithMany()
                   .HasForeignKey(r => r.RevieweeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Order
            builder.HasOne(r => r.Order)
                   .WithMany()
                   .HasForeignKey(r => r.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}