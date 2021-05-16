using GuestApp.Extentions;
using GuestApp.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GuestApp.Services
{
    public class CustomizedListService
    {
        private static ObservableCollection<DTO.Guest> _customizedList = new ObservableCollection<DTO.Guest>();
        public static ObservableCollection<DTO.Guest> CustomizedList(object obj)
        {
            var selectedGuests = (IEnumerable<object>)obj;
            foreach (var guest in selectedGuests)
            {
                DTO.Guest selectedGuest = guest as DTO.Guest;
                if (selectedGuest != null)
                    if (_customizedList.Any(c => c.Id == selectedGuest.Id) != true && selectedGuest.Id != 0)
                        _customizedList.Add(selectedGuest);
            }
            return Counter.AddCounterToGuestList(_customizedList.ToList()).ToObservableCollection();
        }
    }
}
