namespace GuestApp.Model
{
    using System.Collections.Generic;

    public partial class City
    {
        public City()
        {
            Guests = new HashSet<Guest>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Guest> Guests { get; set; }
    }
}