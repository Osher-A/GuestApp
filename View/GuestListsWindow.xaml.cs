using GuestApp.DAL;
using GuestApp.DTO;
using GuestApp.Services;
using GuestApp.ViewModel;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GuestApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GuestListsWindow : Window
    {
        public GuestListsWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            GuestListWindowViewModel.CloseListWindow += CloseWindow;
            GuestListWindowViewModel.LabelsWindowHandler += OpenLabelsWindow;
        }

        private void OpenLabelsWindow(List<Guest> guestList)
        {
            var labelsVm = new LabelWindowViewModel(guestList);
            var labelsWindow = new LabelsWindow();
            labelsWindow.DataContext = labelsVm;
            labelsWindow.Show();
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
