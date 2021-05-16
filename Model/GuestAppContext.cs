namespace GuestApp.Model
{
    using GuestApp.Configurations;
    using Microsoft.EntityFrameworkCore;

    public partial class GuestAppContext : DbContext
    {
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventGuest> EventsGuests { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Database=GuestBook;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GuestConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new RegionConfiguration());
            modelBuilder.ApplyConfiguration(new EventGuestConfiguration());

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity("EventGuest")
            //    .ToTable("EventsGuests");
            //    .Property("EventsId")
            //    .HasColumnName("EventId");

            //modelBuilder.Entity("EventGuest")
            //    .Property("GuestsId")
            //    .HasColumnName("GuestId");

            //modelBuilder.Entity<Event>()
            //    .HasOne(e => e.User)
            //    .WithMany(u => u.Events)
            //    .HasForeignKey(e => e.UserId);
        }
    }
}