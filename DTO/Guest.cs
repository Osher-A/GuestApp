using GuestApp.DTO;
using GuestApp.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;



namespace GuestApp.DTO
{
    public class Guest : INotifyPropertyChanged, IGuest
    {
        public int Id { get; set; }
        public int GuestPositionInList { get; set; }
        private GuestTitle _guestTitle;
        public GuestTitle GuestTitle
        {
            get { return _guestTitle; }
            set
            {
                _guestTitle = value;
                OnPropertyChanged("GuestTitle");
            }
        }
        private string _firstNames;
        public string FirstNames
        {
            get
            {
                if (_firstNames != null && _firstNames.ToLower().Contains("and"))
                    _firstNames = _firstNames.ToLower().Replace("and", "&");
                if (_firstNames != null && !string.IsNullOrWhiteSpace(_firstNames))
                    _firstNames = nameFormat(_firstNames);
                return _firstNames;
            }
            set
            { _firstNames = value; }
        }

        private string _lastName;

        public string LastName
        {
            get
            {
                {
                    if (_lastName != null)
                        _lastName = nameFormat(_lastName);
                    return _lastName;
                }
            }
            set { _lastName = value; }
        }

        private string _fullName;


        public string FullName
        {
            get
            {
                if (FirstNames == null && LastName == null)
                    return _fullName;
                else
                    return FirstNames + LastName;
            }
            set { _fullName = value; }
        }

        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Region { get; set; }
        public virtual string City { get; set; }
        public string Zip { get; set; }
        public bool IsFamily { get; set; }


        public string Initials
        {
            get
            {
                string initials = "";
                if (FirstNames.Contains("&"))
                    initials = GetCouplesInitials();
                else initials += FirstNames[0];
                return initials.ToUpper();
            }
        }

        private string GetCouplesInitials()
        {
            string[] couplesNames = FirstNames.Split('&');
            string[] husbandsNames = couplesNames[0].Trim().Split(' ');
            string[] wifesNames = couplesNames[1].Trim().Split(' ');
            string husbandsInitials = GetFirstLetters(husbandsNames);
            string wifesInitials = GetFirstLetters(wifesNames);
            return husbandsInitials + "& " + wifesInitials.TrimEnd();
        }

        private static string GetFirstLetters(string[] spousesNames)
        {
            string initials = string.Empty;
            for (int i = 0; i < spousesNames.Length; i++)
            {
                string name = spousesNames[i];
                initials += name[0];
                if (name == spousesNames[spousesNames.Length - 1])
                    initials += " ";
            }

            return initials;
        }

        private string nameFormat(string guestName)
        {
            string formatedName = string.Empty;
            string[] names = guestName.Trim().ToLower().Split(' ');
            foreach (var name in names)
            {
                string firstLetter = string.Empty;
                if (name != string.Empty)
                    firstLetter = FirstLetterToUpper(name, names);
                formatedName += firstLetter;
                formatedName += RestOfNameToLower(names, name);
            }
            return formatedName;
        }

        private string FirstLetterToUpper(string name, string[] names)
        {
            string upperCaseLetters = name.ToUpper();
            if (name.Contains('.') && names[names.Length - 1] != name)
                upperCaseLetters += " ";
            else if (!name.Contains('.'))
                upperCaseLetters = name[0].ToString().ToUpper();
            return upperCaseLetters;
        }

        private static string RestOfNameToLower(string[] names, string name)
        {
            string nameToLower = "";
            if (!name.Contains('.'))
            {
                for (int i = 1; i < name.Length; i++)
                    nameToLower += name[i];

                if (names[names.Length - 1] != name)
                    nameToLower += " ";
            }
            return nameToLower;
        }
        public List<Event> Events { get; set; } = new List<Event>();
        public event PropertyChangedEventHandler PropertyChanged;


        //public override bool Equals(object obj)
        //{
        //    var cast = obj as Guest;
        //    if (cast == null)
        //        return false;
        //    return cast.Id == this.Id
        //        && cast.FullName == this.FullName
        //        && cast.HouseNumber == this.HouseNumber
        //        && cast.Street == this.Street
        //        && cast.Region == this.Region
        //        && cast.City == this.City
        //        && cast.Zip == this.Zip;
        //}

        //public override int GetHashCode()
        //{
        //    return this.Id;
        //}

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
