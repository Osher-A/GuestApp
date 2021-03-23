using GuestApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using GuestApp.Utility;
using System.Data.Entity;
using System.Windows;

namespace GuestApp.DAL
{
    public class EventRepository : IEventRepository
    {
        public static DTO.User User;
        public EventRepository(DTO.User user = null)
        {
            User = user;
        }
        public void AddEvent(DTO.Event newEvent)
        {
            var modelEvent = Mapper.EventMapper(newEvent, new Model.Event());
            try
            {
                using (var db = new GuestAppContext())
                {
                    modelEvent.UserId = User.Id;
                    db.Events.Add(modelEvent);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditEvent(DTO.Event eventToEdit, DTO.Event modifiedEvent)
        {
            try
            {
                using (var db = new GuestAppContext())
                {
                    var modelEvent = db.Events.Find(eventToEdit.Id);
                    modelEvent.Name = modifiedEvent.Name;
                    modelEvent.Date = modifiedEvent.Date;

                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteEvent(Model.Event eventToDelete)
        {
            try
            {
                using (var db = new GuestAppContext())
                {
                   var eventToRemove = db.Events.Find(eventToDelete.Id);
                    db.Events.Remove(eventToRemove);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Event> GetAllEvents()
        {
            using (var db = new GuestAppContext())
            {
                return db.Events.Where(e => e.UserId == User.Id).ToList(); ;
            }
        }
    }
}
