using GuestApp.Interfaces;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GuestApp.Model
{
    public class Event : IEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public virtual List<EventGuest> EventGuests { get; set; }
        public virtual User User { get; set; }
        public string UserId {get; set;}


        public Event()
        {
            EventGuests = new List<EventGuest>();
            User = new User();
        }
    }
}
