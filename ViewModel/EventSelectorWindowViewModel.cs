using GuestApp.DAL;
using GuestApp.DTO;
using GuestApp.Services;
using GuestApp.Utility;
using GuestApp.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GuestApp.ViewModel
{
    public class EventSelectorWindowViewModel : INotifyPropertyChanged
    {
        private string _userId;
        private Event _currentEvent;
        private IEventRepository _eventRepository;
        private EventDataService _eventDataService;
        private Visibility _eventSelectorWindowVisibility = Visibility.Visible;
        private const string _comboBoxDefaultText = "Please select an Event";
        public Visibility EventSelectorWindowVisibility
        {
            get { return _eventSelectorWindowVisibility; }
            set
            {
                _eventSelectorWindowVisibility = value;
                RaisePropertyChanged("EventSelectorWindowVisibility");
            }
        }
        private Visibility _createAndEditEventWindowVisibility = Visibility.Collapsed;
        public Visibility CreateAndEditEventWindowVisibility
        {
            get { return _createAndEditEventWindowVisibility; }
            set
            {
                _createAndEditEventWindowVisibility = value;
                EventSelectorWindowVisibility = (_createAndEditEventWindowVisibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
                RaisePropertyChanged("CreateAndEditEventWindowVisibility");
            }
        }

        private DTO.Event _selectedEvent;
        public DTO.Event SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                RaisePropertyChanged("SelectedEvent");
            }
        }
        private ObservableCollection<string> _eventsNames;
        public ObservableCollection<string> EventsNames
        {
            get { return _eventsNames; }
            set 
            {
                _eventsNames = value;
                RaisePropertyChanged("EventsNames");
            }
        }

        private DTO.Event _eventToEdit;
        private bool _selectionValidated;
        private bool _canEditEventDetails;
        private bool _canCreateNewEvent;

        public ICommand EditDialogCommand { get; set; }
        public ICommand NewEventDialogCommand { get; set; }
        public ICommand EventGuestListsCommand { get; set; }
        public ICommand NewEventCommand { get; set; }
        public ICommand EditEventCommand { get; set; }
        public ICommand DeleteEventCommand { get; set; }
        public ICommand ClearTextBoxCommand { get; set; }
        public ICommand VisibilityCommand { get; set; }
        public ICommand ValidateSelectionCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public static Action<string,Event> ListDialogHandler;
        
         public EventSelectorWindowViewModel(IEventRepository eventRepository)
        {
            _userId = EventRepository.User.Id;
            _eventRepository = eventRepository;
            _eventDataService = new EventDataService(_eventRepository);
            SelectedEvent = new Event();
            LoadEventNames();
            EventGuestListsCommand = new CustomCommand(SelectedEventLists, CanSelectEventList);
            NewEventDialogCommand = new CustomCommand(NewEventDialog, CanCreateNewEventDialog);
            EditDialogCommand = new CustomCommand(EditDialog, CanOpenEditDialog);
            NewEventCommand = new CustomCommand(CreateNewEvent, CanCreateNewEvent);
            DeleteEventCommand = new CustomCommand(DeleteEvent, CanDeleteEvent);
            ClearTextBoxCommand = new CustomCommand(ClearDateTextBox, CanClearDateTextBox);
            EditEventCommand = new CustomCommand(EditEvent, CanEditEvent);
            VisibilityCommand = new CustomCommand(ChangeVisibility, CanChangeVisibility);
            ValidateSelectionCommand = new CustomCommand(ValidateSelection, CanValidateSelection);
        }

        private bool CanSelectEventList(object obj)
        {
            //The second option is needed to cover for a case when the comboBox wasn't selected but a new event has been created

           if (_selectionValidated == true || (SelectedEvent.Name != _comboBoxDefaultText && SelectedEvent.Name != null) )
                return true;
            else
                return false;
        }

        private void SelectedEventLists(object obj)
        {
            UpdateCurrentEvent();

            //When one comes back to the event selector it should be visible.
            CreateAndEditEventWindowVisibility = Visibility.Collapsed;

            if (ListDialogHandler != null)
                ListDialogHandler(_userId,_currentEvent);
        }

        private bool CanCreateNewEventDialog(object obj)
        {
            return true;
        }

        private void NewEventDialog(object obj)
        {
            SelectedEvent.Name = null;
            SelectedEvent.Date = null;
            _canCreateNewEvent = true;
            _canEditEventDetails = false;
            _selectionValidated = false;
            CreateAndEditEventWindowVisibility = Visibility.Visible;
        }

        private bool CanOpenEditDialog(object obj)
        {
            if (_selectionValidated)
                return true;
            else
                return false;
        }

        private void EditDialog(object obj)
        {
            _eventToEdit = _eventDataService.GetEvent(SelectedEvent.Name);

            // The Selected event details should appear in the edit dialog.
            SelectedEvent.Name = _eventToEdit.Name;
            SelectedEvent.Date = _eventToEdit.Date;

            _canEditEventDetails = true;
            _canCreateNewEvent = false;
            _selectionValidated = false;
            CreateAndEditEventWindowVisibility = Visibility.Visible;
        }

        private bool CanCreateNewEvent(object obj)
        {
            if (_canCreateNewEvent && !_canEditEventDetails && SelectedEvent.Name != null)
                return true;
            else
                return false;
        }

        private void CreateNewEvent(object obj)
        {
            _eventDataService.AddEvent(SelectedEvent);

            //The order here is critical because after LoadEvenNames() is called, the SelectedEvent gets changed. The original
            //SelectedEvent is needed later in order to be able to edit immediately after creating the new event. 

            var newEvent = _eventDataService.GetEvent(SelectedEvent.Name);
            LoadEventNames();
            SelectedEvent.Name = newEvent.Name;
            SelectedEvent.Date = newEvent.Date;
            _eventToEdit = _eventDataService.GetEvent(SelectedEvent.Name);

            _canEditEventDetails = true;
            _canCreateNewEvent = false;
        }

        private bool CanDeleteEvent(object obj)
        {
            if (_selectionValidated)
                return true;
            else return false;
        }

        private void DeleteEvent(object obj)
        {
            int selectedIndex = (int)obj;
           var usersConfirmation = MessageBox.Show(string.Format("Are you sure you would like to delete all {0} details?", SelectedEvent.Name), "Warning!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (usersConfirmation == MessageBoxResult.OK)
            {
                 _eventDataService.DeleteEvent(SelectedEvent.Name);
                EventsNames.RemoveAt(selectedIndex);

                LoadEventNames();
            }
        }
        private bool CanClearDateTextBox(object obj)
        {
            if (SelectedEvent.Name != null)
                return true;
            else
                return false;
        }

        private void ClearDateTextBox(object obj)
        {
            SelectedEvent.Date = new DateTime();
        }

        private bool CanEditEvent(object obj)
        {
            if (_canEditEventDetails && !_canCreateNewEvent)
                return true;
            else
                return false;
        }

        private void EditEvent(object obj)
        {
            _eventDataService.EditEvent(_eventToEdit, SelectedEvent);
            var editedEvent = _eventDataService.GetEvent(SelectedEvent.Name);
            LoadEventNames();
            SelectedEvent.Name  = editedEvent.Name;
            SelectedEvent.Date = editedEvent.Date;

            _canEditEventDetails = false;
        }

        private bool CanChangeVisibility(object obj)
        {
            return true;
        }

        private void ChangeVisibility(object obj)
        {
            LoadEventNames();
            CreateAndEditEventWindowVisibility = Visibility.Collapsed;
        }

        private bool CanValidateSelection(object obj)
        {
            return true;
        }

        private void ValidateSelection(object obj)
        {
            int selectedIndex = (int)obj;
            if (selectedIndex == 0)
                _selectionValidated = false;
            else
                _selectionValidated = true;
        }

        private void UpdateCurrentEvent()
        {
            var currentEvent = new EventSelectorService(_eventRepository).GetCurrentEvent(SelectedEvent.Name);
            if (currentEvent != null)
                _currentEvent = currentEvent;
            else
                _currentEvent = SelectedEvent;
        } 

        private void LoadEventNames()
        {
            EventsNames = new EventSelectorService(_eventRepository).EventsNames;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
