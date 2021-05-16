using System.Collections.Generic;
using System.Linq;

namespace GuestApp.Utility
{
    public class Counter
    {
        public static List<DTO.Guest> AddCounterToGuestList(List<DTO.Guest> guestList)
        {
            guestList = guestList.OrderBy(c => c.LastName).ToList();
            int counter = 0;
            foreach (var guest in guestList)
            {
                counter++;
                guest.GuestPositionInList = counter;
            }
            return guestList;
        }
    }
}