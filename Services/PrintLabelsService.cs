using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GuestApp.Services
{
    public class PrintLabelsService
    {
        public static void PrintLabels(object obj)
        {
            PrintDialog pd = new PrintDialog();
            //pd.SelectedPagesEnabled = true;
            //pd.UserPageRangeEnabled = true;
            if (pd.ShowDialog() == true)
            {
                FlowDocument fd = (FlowDocument)obj;
                fd.PageHeight = pd.PrintableAreaHeight;
                fd.PageWidth = pd.PrintableAreaWidth;
                fd.PagePadding = new Thickness(50);
                fd.ColumnGap = 0;
                fd.ColumnWidth = pd.PrintableAreaWidth;

                IDocumentPaginatorSource dps = fd;
                pd.PrintDocument(dps.DocumentPaginator, "Labels");
            }
        }
    }
}