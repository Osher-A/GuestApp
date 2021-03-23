using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestApp.Services
{
    public class SaveAsService
    {
        public static void SaveAs(List<DTO.Guest> list)
        {
            var sb = new StringBuilder();
            foreach (var guest in list)
            {
                var guest1 = (DTO.Guest)guest;
                sb.Append("Name: ");
                sb.Append(guest1.FullName.PadRight(45, ' '));
                sb.Append("Address: ");
                sb.Append(guest1.HouseNumber.PadRight(4, ' '));
                sb.Append(guest1.Street.PadRight(35,' '));
                sb.Append(guest1.City);
                sb.Append("  ");
                sb.Append(guest1.Zip);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }
            var saveDlg = new SaveFileDialog { Filter = "Text Files |*.txt; | All Files |*.*" };
            if (true == saveDlg.ShowDialog())
            {
                File.WriteAllText(saveDlg.FileName, sb.ToString());
            }
        }
    }
}

