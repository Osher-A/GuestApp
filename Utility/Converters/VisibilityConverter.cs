using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GuestApp.Utility.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int numberOfGroups = int.Parse(value.ToString());
            if ( numberOfGroups >= int.Parse(parameter as string))
                return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
