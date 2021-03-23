using System.Collections.Generic;

namespace GuestApp.DAL
{
    public interface IEventRepository
    {
        void AddEvent(DTO.Event newEvent);
        void EditEvent(DTO.Event eventToEdit, DTO.Event modifiedEvent);
        void DeleteEvent(Model.Event eventToDelete);
        List<Model.Event> GetAllEvents();
    }
}