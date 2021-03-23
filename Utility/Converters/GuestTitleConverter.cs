using GuestApp.Interfaces;
using System;
using System.Globalization;
using System.Windows.Data;

namespace GuestApp.Utility.Converters
{
    public class GuestTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string title;
            switch ((GuestTitle)value)
            {
                case GuestTitle.MrAndMrs:
                    title = "Mr & Mrs";
                    break;
                case GuestTitle.Mr:
                    title = "Mr";
                    break;
                case GuestTitle.Mrs:
                    title = "Mrs";
                    break;
                case GuestTitle.RabbiAndMrs:
                    title = "Rabbi & Mrs";
                    break;
                case GuestTitle.Rabbi:
                    title = "Rabbi";
                    break;
                case GuestTitle.DrAndMrs:
                    title = "Dr & Mrs";
                    break;
                case GuestTitle.Dr:
                    title = "Dr";
                    break;
                case GuestTitle.Ms:
                    title = "Ms";
                    break;
                default:
                    goto case GuestTitle.MrAndMrs;
            }

            return title;
        }

        public Object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GuestTitle title;
            switch (value)
            {
                case "Mr & Mrs":
                    title = GuestTitle.MrAndMrs;
                    break;
                case "Mr":
                    title = GuestTitle.Mr;
                    break;
                case "Mrs":
                    title = GuestTitle.Mrs;
                    break;
                case "Rabbi & Mrs":
                    title = GuestTitle.RabbiAndMrs;
                    break;
                case "Rabbi":
                    title = GuestTitle.Rabbi;
                    break;
                case "Dr & Mrs":
                    title = GuestTitle.DrAndMrs;
                    break;
                case "Dr":
                    title = GuestTitle.Dr;
                    break;
                case "Ms":
                    title = GuestTitle.Ms;
                    break;
                default:
                    goto case "Mr & Mrs";
            }
            return title;
        }
    }
}
