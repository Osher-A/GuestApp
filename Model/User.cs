using GuestApp.Interfaces;
using System.Collections.Generic;

namespace GuestApp.Model
{
    public class User : IUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public virtual List<Event> Events { get; set; }

        public User()
        {
            Events = new List<Event>();
        }
    }
}