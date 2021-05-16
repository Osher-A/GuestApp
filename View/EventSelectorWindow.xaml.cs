using GuestApp.DAL;
using GuestApp.DTO;
using GuestApp.Services;
using GuestApp.ViewModel;
using System.Windows;

namespace GuestApp.View
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>S
    public partial class EventSelectorWindow : Window
    {
        public EventSelectorWindow()
        {
            InitializeComponent();
            EventSelectorWindowViewModel.ListDialogHandler += OpenListDialog;
        }

        private void OpenListDialog(string userId, Event currentEvent)
        {
            var listsDialog = new GuestListsWindow();
            listsDialog.DataContext = new GuestListWindowViewModel(new GuestRepository(userId, currentEvent), new UsersMessageService(), currentEvent);
            listsDialog.ShowDialog();
        }
    }
}