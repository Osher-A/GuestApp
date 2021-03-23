namespace GuestApp.Model
{
    using System.Collections.Generic;

    public partial class Region
    {
        public Region()
        {
            Guests = new HashSet<Guest>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Guest> Guests { get; set; }
    }
}
