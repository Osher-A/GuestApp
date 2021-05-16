using GuestApp.Interfaces;
using System.Collections.Generic;

namespace GuestApp.Model
{
    public partial class Guest : IGuest
    {
        public int Id { get; set; }

        public string FirstNames { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string HouseNumber { get; set; }

        public string Street { get; set; }

        public string Zip { get; set; }

        public bool IsFamily { get; set; }

        public GuestTitle GuestTitle { get; set; }

        public int CityId { get; set; }

        public int? RegionId { get; set; }

        public virtual City City { get; set; }

        public virtual Region Region { get; set; }

        public virtual List<EventGuest> GuestEvents { get; set; }

        public Guest()
        {
            GuestEvents = new List<EventGuest>();
        }
    }
}