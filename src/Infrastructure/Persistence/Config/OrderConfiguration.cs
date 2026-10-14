using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    namespace Infrastructure.Persistence.Config
    {
        public class OrderConfiguration : IEntityTypeConfiguration<Order>
        {
            public void Configure(EntityTypeBuilder<Order> builder)
            {
                builder.HasKey(o => o.Id);

                builder.Property(o => o.Id)
                       .ValueGeneratedOnAdd();

                builder.Property(o => o.ProblemDescription).HasMaxLength(1000).IsRequired();

                builder.HasOne(o => o.User)
                       .WithMany(u => u.Orders)
                       .HasForeignKey(o => o.UserId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(o => o.Address)
                       .WithMany()
                       .HasForeignKey(o => o.AddressId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(o => o.Category)
                       .WithMany()
                       .HasForeignKey(o => o.CategoryId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(o => o.Offer)
                       .WithMany()
                       .HasForeignKey(o => o.AcceptedOfferId)
                       .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
