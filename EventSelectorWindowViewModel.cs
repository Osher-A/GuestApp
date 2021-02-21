using GuestApp.DTO;
using GuestApp.Services;
using GuestApp.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GuestApp.ViewModel
{
    public class EventSelectorWindowViewModel : INotifyPropertyChanged
    {
        private Visibility _visibility;
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            { 
                _visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        private DTO.Event _event;
        public DTO.Event Event
        {
            get { return _event; }
            set 
            {
                _event = value;
                View.App.Event = _event;
                OnPropertyChanged("Event");
            }
        }

        public ObservableCollection<DTO.Event> Events { get; set; } = new ObservableCollection<Event>();
        public ICommand NewEventCommand { get; set; }
        public ICommand SelectedEventListsCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public EventSelectorWindowViewModel()
        {
            var eventsService = new EventSelectorService();
            Events = eventsService.GetListOfEvents();
            SelectedEventListsCommand = new CustomCommand(SelectedEventLists, canSelectEvent);
            NewEventCommand = new CustomCommand(newEvent, canCreateNewEvent);
        }


        private bool canSelectEvent(object obj)
        {
            return true;
        }
        private void SelectedEventLists(object obj)
        {
        }

        private bool canCreateNewEvent(object obj)
        {
            throw new NotImplementedException();
        }
        private void newEvent(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
