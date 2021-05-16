using GuestApp.DAL;
using GuestApp.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GuestApp.Services
{
    public class EventSelectorService
    {
        private IEventRepository _eventRepository;
        private List<Model.Event> _modelEvents;
        public EventSelectorService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _modelEvents = _eventRepository.GetAllEvents();
        }
        public DTO.Event GetCurrentEvent(string eventName)
        {
            var currentModelEvent = _modelEvents.Find(me => me.Name == eventName);
            var currentDTOEvent = Mapper.EventMapper(currentModelEvent, new DTO.Event());

            return currentDTOEvent;
        }
        private ObservableCollection<string> _eventsNames;
        public ObservableCollection<string> EventsNames
        {
            get
            {
                _eventsNames = GetEventsNames();
                return _eventsNames;
            }
        }
        private ObservableCollection<string> GetEventsNames()
        {
            var events = GetListOfEvents();
            var eventsNames = new ObservableCollection<string>();
            eventsNames.Add("Please select an Event");

            foreach (var Event in events)
                eventsNames.Add(Event.Name);
            return eventsNames;
        }

        private ObservableCollection<DTO.Event> GetListOfEvents()
        {
            var dtoEvents = ToDtoEvents(_modelEvents);
            return dtoEvents;
        }

        private ObservableCollection<DTO.Event> ToDtoEvents(List<Model.Event> modelEvents)
        {
            var dtoEvents = new ObservableCollection<DTO.Event>();

            foreach (var Event in modelEvents)
                dtoEvents.Add(Mapper.EventMapper(Event, new DTO.Event()));

            return dtoEvents;
        }
    }
}
