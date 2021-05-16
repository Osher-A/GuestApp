using GuestApp.DAL;
using GuestApp.DTO;
using GuestApp.ViewModel;
using System.Windows;

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
            LoginWindowViewModel.EventSelectorWindowHandler += OpenEventSelectWindow;
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