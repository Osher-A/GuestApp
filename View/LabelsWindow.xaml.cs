using GuestApp.View.CustomControls;
using GuestApp.ViewModel;
using System;
using System.Windows;
using System.Windows.Documents;

namespace GuestApp.View
{
    /// <summary>
    /// Interaction logic for LabelsWindow.xaml
    /// </summary>
    public partial class LabelsWindow : Window
    {
        public LabelsWindow()
        {
            InitializeComponent();

            AddMoreSheetsOfLables();

            LabelWindowViewModel.CloseWindow += Vm_CloseWindow;
        }

        private void AddMoreSheetsOfLables()
        {
            foreach (var group in LabelWindowViewModel.GroupsOfGuests)
            {
                var sheetOfLabels = new SheetOfLabels();
                sheetOfLabels.ListView.ItemsSource = group;
                BlockUIContainer bC = new BlockUIContainer();
                bC.Padding = new Thickness(0, 37, 0, 0);
                bC.Child = sheetOfLabels;
                LabelsFD.Blocks.Add(bC);
            }
        }

        private void Vm_CloseWindow(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}