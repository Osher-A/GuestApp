using GuestApp.DTO;
using GuestApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;

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

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
