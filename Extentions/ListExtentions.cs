using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
