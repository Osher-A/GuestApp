using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GuestApp.Extentions
{
    public static class ListExtentions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            var guestList = new ObservableCollection<T>();
            if (list != null)
            {
                foreach (var guest in list)
                    guestList.Add(guest);
            }
            return guestList;
        }
    }
}
