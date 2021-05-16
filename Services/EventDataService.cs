using GuestApp.DAL;
using GuestApp.DTO;
using GuestApp.Utility;
using System.Collections.Generic;
using System.Linq;

namespace GuestApp.Services
{
    public class EventDataService
    {
        private IEventRepository _eventRepository;
        private List<Model.Event> _events;

        public EventDataService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _events = _eventRepository.GetAllEvents();
        }
        public void AddEvent(DTO.Event Event)
        {
            if (!_events.Exists(e => e.Name.ToUpper().Trim() == Event.Name.ToUpper().Trim()))
            {
                _eventRepository.AddEvent(Event);
                _events = _eventRepository.GetAllEvents();
            }
        }

        public void EditEvent(DTO.Event eventToEdit, DTO.Event modifiedEvent)
        {
            var modelEventToEdit = _events.Find(e => e.Name == eventToEdit.Name);
            if (modelEventToEdit != null)
                _eventRepository.EditEvent(eventToEdit, modifiedEvent);
            _events = _eventRepository.GetAllEvents();
        }

        public void DeleteEvent(string eventName)
        {
            var modelEventToDelete = _events.SingleOrDefault(e => e.Name == eventName);
            if (modelEventToDelete != null)
                _eventRepository.DeleteEvent(modelEventToDelete);
            _events = _eventRepository.GetAllEvents();
        }

        public DTO.Event GetEvent(string eventToFind)
        {
            var modelEvent = _events.Find(e => e.Name == eventToFind);
            var dtoEvent = Mapper.EventMapper(modelEvent, new DTO.Event());

            return dtoEvent;

        }
    }
}
