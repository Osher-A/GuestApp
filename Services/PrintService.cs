using System.Windows.Controls;
using System.Windows.Media;

namespace GuestApp.Services
{
    public class PrintService
    {
        public static void PrintVisual(object visualList)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual((Visual)visualList, "My First Print Job");
            }
        }
    }
}