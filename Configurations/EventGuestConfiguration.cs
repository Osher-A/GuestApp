using GuestApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuestApp.Configurations
{
    public class EventGuestConfiguration : IEntityTypeConfiguration<EventGuest>
    {
        public void Configure(EntityTypeBuilder<EventGuest> builder)
        {
            builder.HasKey(eg => new { eg.EventId, eg.GuestId });

            builder.HasOne(eg => eg.Event)
            .WithMany(e => e.EventGuests)
            .HasForeignKey(eg => eg.EventId);

            builder.HasOne(eg => eg.Guest)
            .WithMany(g => g.GuestEvents)
            .HasForeignKey(eg => eg.GuestId);
        }
    }
}