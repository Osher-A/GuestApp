using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace GuestApp.Utility.Converters
{
    public class GuestGroupConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            int groupIndex = int.Parse(parameter.ToString());
            var allGroups = (ObservableCollection<ObservableCollection<DTO.Guest>>)value;
            if (allGroups.Count == 0 || allGroups.Count <= groupIndex)
                return null;
            else 
            return allGroups[groupIndex];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
