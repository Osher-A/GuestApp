using GuestApp.DAL;
using GuestApp.DTO;
using GuestApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GuestApp.View
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
            LoginWindowViewModel.RegisterSuccessful += OpenEventSelectWindow;
        }

        private void OpenEventSelectWindow(User user)
        {
            var eventWindow = new EventSelectorWindow();
            eventWindow.DataContext = new EventSelectorWindowViewModel(new EventRepository(user));
            eventWindow.Show();
            this.Close();
        }
    }
}
