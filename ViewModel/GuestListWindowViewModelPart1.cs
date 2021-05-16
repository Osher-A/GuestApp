using GuestApp.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GuestApp.ViewModel
{
    public partial class GuestListWindowViewModel : INotifyPropertyChanged
    {
        private Event _currentEvent;

        public Event CurrentEvent
        {
            get { return _currentEvent; }
            set
            {
                _currentEvent = value;
                RaisePropertyChanged("CurrentEvent");
            }
        }

        private int _selectedTabItem;

        public int SelectedTabItem
        {
            get { return _selectedTabItem; }
            set
            {
                _selectedTabItem = value;
                RaisePropertyChanged("SelectedTabItem");
            }
        }

        private ObservableCollection<DTO.Guest> _allGuests;

        public ObservableCollection<DTO.Guest> AllGuests
        {
            get { return _allGuests; }
            set
            {
                _allGuests = value;
                RaisePropertyChanged("AllGuests");
            }
        }

        private ObservableCollection<DTO.Guest> _eventGuests;

        public ObservableCollection<DTO.Guest> EventGuests
        {
            get { return _eventGuests; }
            set
            {
                _eventGuests = value;
                RaisePropertyChanged("EventGuests");
            }
        }

        private ObservableCollection<DTO.Guest> _customizedList;

        public ObservableCollection<DTO.Guest> CustomizedList
        {
            get { return _customizedList; }
            set
            {
                _customizedList = value;
                RaisePropertyChanged("CustomizedList");
            }
        }

        private ObservableCollection<DTO.Guest> _searchGuests;

        public ObservableCollection<DTO.Guest> SearchGuests
        {
            get { return _searchGuests; }
            set
            {
                _searchGuests = value;
                RaisePropertyChanged("SearchGuests");
            }
        }

        public static ObservableCollection<DTO.Guest> LabelGuests { get; private set; }

        private DTO.Guest _selectedGuest = new DTO.Guest();

        public DTO.Guest SelectedGuest
        {
            get { return _selectedGuest; }
            set
            {
                _selectedGuest = value;
                RaisePropertyChanged("SelectedGuest");
            }
        }

        private DTO.Guest _newGuest = new DTO.Guest();

        public DTO.Guest NewGuest
        {
            get { return _newGuest; }
            set
            {
                _newGuest = value;
                RaisePropertyChanged("NewGuest");
            }
        }

        private DTO.Guest _searchGuest = new DTO.Guest();

        public DTO.Guest SearchGuest
        {
            get { return _searchGuest; }
            set
            {
                _searchGuest = value;
                RaisePropertyChanged("SearchGuest");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}