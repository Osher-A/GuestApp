using GuestApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuestApp.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.Property(r => r.Id)
                 .UseIdentityColumn(0, 1);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}