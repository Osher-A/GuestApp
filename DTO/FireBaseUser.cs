namespace GuestApp.DTO
{
    public class FireBaseUser : User
    {
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public string Password2 { get; set; }
        public string NewPassword { get; set; }
        public string IdToken { get; set; }
    }
}
