using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GuestApp.Services
{
    public interface IUsersMessageService
    {
        void EmptyFieldsAlert();

        void GuestExistsElert();

        bool UsersEditConfirmation(List<string> changes);
    }

    public class UsersMessageService : IUsersMessageService
    {
        public void EmptyFieldsAlert()
        {
            MessageBox.Show("Please fill in all the required fields!",
                "Information missing", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void GuestExistsElert()
        {
            MessageBox.Show("Sorry that address is already used !", "Existing Guest", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool UsersEditConfirmation(List<string> changes)
        {
            return ChangesAlert(EditMessage(changes));
        }

        private static bool ChangesAlert(string message)
        {
            var messageResult = MessageBox.Show(message, "Confirm changes", MessageBoxButton.OKCancel,
                MessageBoxImage.Question);
            if (messageResult == MessageBoxResult.OK)
                return true;
            else return false;
        }

        private static string EditMessage(List<string> changes)
        {
            string message = "Are you sure you want to Edit the";
            for (int i = 0; i < changes.Count; i++)
            {
                message += changes.ElementAt(i);
                if (changes.Count > i + 2)
                    message += ",";
                else if (changes.Count > i + 1)
                    message += " and";
            }
            message += "?";
            return message;
        }
    }
}