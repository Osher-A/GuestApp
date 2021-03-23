using GuestApp.Extentions;
using GuestApp.Interfaces;
using GuestApp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
