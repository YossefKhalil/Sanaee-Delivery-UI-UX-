using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Config
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.AddressId);

            builder.Property(a => a.City).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Region).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Street).HasMaxLength(200).IsRequired();
            builder.Property(a => a.BuildingNumber).HasMaxLength(20);

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Addresses)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
