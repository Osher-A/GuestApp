using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace GuestApp.Utility.Converters
{
    public class DaysRemainingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateOfEvent = new DateTime();
            Double daysLeft;
            if (value != null)
            {
                dateOfEvent = (DateTime)value;
                daysLeft = dateOfEvent.Subtract(DateTime.Now).TotalDays;
                if (daysLeft < 0)
                    return "(Event has been)";
                else
                    return string.Format("({0:##} Days Remaining)", daysLeft);
            }
            else
                return "(Date has not been set)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
