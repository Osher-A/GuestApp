using GuestApp.DAL;
using GuestApp.DTO;
using GuestApp.Services;
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
    /// </summary>S
    public partial class EventSelectorWindow : Window
    {
        public EventSelectorWindow()
        {
            InitializeComponent();
            EventSelectorWindowViewModel.ListDialogHandler += OpenListDialog;
        }

        private void OpenListDialog(string userId,Event currentEvent)
        {
            var listsDialog = new GuestListsWindow();
            listsDialog.DataContext = new GuestListWindowViewModel(new GuestRepository(userId,currentEvent), new UsersMessageService(), currentEvent);
            listsDialog.Show();
        }
    }
}
