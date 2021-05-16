using GuestApp.DTO;
using GuestApp.Services;
using GuestApp.Utility;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GuestApp.ViewModel
{
    public class LoginWindowViewModel : INotifyPropertyChanged
    {
        private FireBaseUser _user = new FireBaseUser();
        public FireBaseUser FireBaseUser
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("FireBaseUser");
            }
        }
        private Visibility _registerWindowVisibility = Visibility.Collapsed;
        public Visibility RegisterWindowVisibility
        {
            get { return _registerWindowVisibility; }
            set
            {
                _registerWindowVisibility = value;
                OnPropertyChanged("RegisterWindowVisibility");
            }
        }
        private Visibility _loginWindowVisibility = Visibility.Visible;
        public Visibility LoginWindowVisibility
        {
            get { return _loginWindowVisibility; }
            set
            {
                _loginWindowVisibility = value;
                OnPropertyChanged("LoginWindowVisibility");
            }
        }
        private Visibility _changePasswordWindowVisibility = Visibility.Collapsed;
        public Visibility ChangePasswordWindowVisibility
        {
            get { return _changePasswordWindowVisibility; }
            set
            {
                _changePasswordWindowVisibility = value;
                OnPropertyChanged("ChangePasswordWindowVisibility");
            }
        }

        private Visibility _progressBarVisibiliy = Visibility.Hidden;

        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibiliy; }
            set
            {
                _progressBarVisibiliy = value;
                OnPropertyChanged("ProgressBarVisibility");
            }
        }

        private string _code;

        public string Code
        {
            get { return _code; }
            set 
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }


        public ICommand RegisterWindowVisibilityCommand { get; set; }
        public ICommand LoginWindowVisibilityCommand { get; set; }
        public ICommand ChangePasswordWindowVisibilityCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public static event Action<FireBaseUser> EventSelectorWindowHandler;

        public LoginWindowViewModel()
        {
            RegisterWindowVisibilityCommand = new CustomCommand(RegisterWindowVisible, CanRegisterWindowBeVisible);
            LoginWindowVisibilityCommand = new CustomCommand(LoginWindowVisible, CanLoginWindowBeVisible);
            ChangePasswordWindowVisibilityCommand = new CustomCommand(ChangePasswordWindowVisible, CanChangePasswordWindowBeVisible);
            RegisterCommand = new CustomCommand(Register, CanRegister);
            LoginCommand = new CustomCommand(Login, CanLogin);
            ChangePasswordCommand = new CustomCommand(ChangePassword, CanChangePassword);
            ForgotPasswordCommand = new CustomCommand(ResetForgottenPassword, CanResetForgottenPassword);
        }

        private bool CanRegisterWindowBeVisible(object obj)
        {
            return true;
        }

        private void RegisterWindowVisible(object obj)
        {
            WindowVisibility(WindowType.RegisterWindow);
        }

        private bool CanLoginWindowBeVisible(object obj)
        {
            return true;
        }

        private void LoginWindowVisible(object obj)
        {
            WindowVisibility(WindowType.LoginWindow);
        }

        private bool CanRegister(object obj)
        {
            if (!string.IsNullOrWhiteSpace(FireBaseUser.Email) && !string.IsNullOrWhiteSpace(FireBaseUser.Password) && !string.IsNullOrWhiteSpace(FireBaseUser.Password2))
                return true;
            else
                return false;
        }

        private async void Register(object obj)
        {
            ToggleProgressBarVisibility();

            if (await LoginService.RegistrationSuccessfull(FireBaseUser))
            {
                OpenEventSelectorWindow();
            }

            ToggleProgressBarVisibility();
        }

        private bool CanLogin(object obj)
        {
            if (!string.IsNullOrWhiteSpace(FireBaseUser.Email) && !string.IsNullOrWhiteSpace(FireBaseUser.Password))
                return true;
            else
                return false;
        }

        private async void Login(object obj)
        {
            ToggleProgressBarVisibility();

            if (await LoginService.LoginSuccessfull(FireBaseUser))
            {
                OpenEventSelectorWindow();
            }

            ToggleProgressBarVisibility();
        }

        private bool CanResetForgottenPassword(object obj)
        {
            return !string.IsNullOrWhiteSpace(_user.Email) && string.IsNullOrWhiteSpace(_user.Password);
        }

        private async void ResetForgottenPassword(object obj)
        {
            if(await LoginService.ForgotPassword(_user))
            {
                MessageBox.Show("Check your email and click on the link to reset your Password",
                    "Reset Password", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private bool CanChangePasswordWindowBeVisible(object obj)
        {
            if (string.IsNullOrWhiteSpace(FireBaseUser.Email) || string.IsNullOrWhiteSpace(FireBaseUser.Password))
                return false;
            else
                return true;
        }

        private async void ChangePasswordWindowVisible(object obj)
        {
            ToggleProgressBarVisibility();

            if (await LoginService.LoginSuccessfull(FireBaseUser))
                WindowVisibility(WindowType.ChangePasswordWindow);

            ToggleProgressBarVisibility();
        }

        private async void ChangePassword(object obj)
        {
            ToggleProgressBarVisibility();
            if (await LoginService.ChangePassword(FireBaseUser))
            {
                OpenEventSelectorWindow();
            }
            ToggleProgressBarVisibility();
        }

        private bool CanChangePassword(object obj)
        {
            if (!string.IsNullOrEmpty(FireBaseUser.NewPassword) && FireBaseUser.NewPassword == FireBaseUser.Password2)
                return true;
            else
                return false;
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OpenEventSelectorWindow()
        {
            if (EventSelectorWindowHandler != null)
                EventSelectorWindowHandler(FireBaseUser); // This event is handled by the EventSelectorWindow View
        }

        private void WindowVisibility(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.RegisterWindow:
                    LoginWindowVisibility = Visibility.Collapsed;
                    RegisterWindowVisibility = Visibility.Visible;
                    break;
                case WindowType.ChangePasswordWindow:
                    LoginWindowVisibility = Visibility.Collapsed;
                    ChangePasswordWindowVisibility = Visibility.Visible;
                    break;
                default:
                    RegisterWindowVisibility = Visibility.Collapsed;
                    ChangePasswordWindowVisibility = Visibility.Collapsed;
                    LoginWindowVisibility = Visibility.Visible;
                    break;
            }
        }

        private void ToggleProgressBarVisibility()
        {
            ProgressBarVisibility = (ProgressBarVisibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }
    }
    public enum WindowType
    {
        LoginWindow,
        RegisterWindow,
        ChangePasswordWindow
    }
}
