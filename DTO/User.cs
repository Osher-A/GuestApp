using GuestApp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GuestApp.DTO
{
    public class User : IUser, INotifyPropertyChanged
    {
        public string Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _email;
        public string Email 
        {
            get { return _email; }
            set 
            {
                _email = value;
                OnPropertyChanged("Email");
            } 
        }

        internal void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
