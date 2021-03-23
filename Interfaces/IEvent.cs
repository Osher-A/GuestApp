using System;
using System.Collections.Generic;
using System.Text;

namespace GuestApp.Interfaces
{
        public interface IEvent
        {
        int Id { get; set; }
        string Name { get; set; }
        DateTime? Date { get; set; }
    }
}
