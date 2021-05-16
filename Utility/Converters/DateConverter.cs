using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GuestApp.Utility.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && (DateTime)value == new DateTime(01, 01, 0001))
                return null;
            else if (value == null)
                return "dd/mm/yyyy";
            else
                return ((DateTime)value).ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dateAsString = (string)value;
            DateTime date;
            bool isValidDate = DateTime.TryParse(dateAsString, out date);

            if (!isValidDate)
            {
                MessageBox.Show("Please enter in the correct Format - dd/mm/yyyy", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return date;
        }
    }
}