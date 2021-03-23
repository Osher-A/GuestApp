using GuestApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuestApp.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
       
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.Property(g => g.Id)
                .UseIdentityColumn();

            builder.Property(g => g.FirstNames)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(g => g.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(g => g.FullName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(g => g.Street)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(g => g.HouseNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(g => g.Zip)
                .IsRequired()
                .HasMaxLength(15); 

        }
    }
}

