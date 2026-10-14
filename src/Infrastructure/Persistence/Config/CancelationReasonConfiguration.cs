using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class CancelationReasonConfiguration : IEntityTypeConfiguration<CancelationReason>
    {
        public void Configure(EntityTypeBuilder<CancelationReason> builder)
        {
            builder.HasKey(c => c.ReasonId);

            builder.Property(c => c.Text).HasMaxLength(250).IsRequired();
        }
    }
}