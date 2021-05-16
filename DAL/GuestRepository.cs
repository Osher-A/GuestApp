using GuestApp.Extentions;
using GuestApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GuestApp.DAL
{
    public class GuestRepository : IGuestRepository
    {
        private string _userId;
        private DTO.Event _currentEvent;
        private Model.Guest _guestToEdit;

        public DTO.Guest UsersSelectedGuestToEdit { get; set; }

        public Model.Guest ModelGuestToEdit
        {
            get
            {
                if (UsersSelectedGuestToEdit != null)
                    try
                    {
                        using (var db = new GuestAppContext())
                        {
                            _guestToEdit = db.Guests.Find(UsersSelectedGuestToEdit.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxError(ex.Message);
                    }

                return _guestToEdit;
            }
        }
        public GuestRepository(string userId = null, DTO.Event currentEvent = null)
        {
            _currentEvent = currentEvent;
            _userId = userId;
        }
        public async Task<List<DTO.Guest>> GetGuestListAsync()
        {
            try
            {
                using (var db = new GuestAppContext())
                {
                    var usersEvents = await db.Events.Where(e => e.UserId == _userId)
                        .Include(e => e.EventGuests)
                          .ThenInclude(eg => eg.Guest)
                            .ThenInclude(g => g.Region)
                         .Include(e => e.EventGuests)
                           .ThenInclude(eg => eg.Guest)
                             .ThenInclude(g => g.City)
                          .ToListAsync();

                    var guestList = new List<Guest>();
                    foreach (var Event in usersEvents)
                    {
                        foreach (var eventGuest in Event.EventGuests)
                            guestList.Add(eventGuest.Guest);
                    }
                    return guestList.OrderBy(g => g.LastName).ToList().ToDTOGuestList();
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
            }
            return null;
        }
        public List<DTO.Guest> GetGuestList()
        {
            try
            {
                using (var db = new GuestAppContext())
                {
                    var usersEventsIds = db.Events.Where(e => e.UserId == _userId).Select(e => e.Id);
                    var eventsGuestsId = db.EventsGuests.Where(eg => usersEventsIds.Contains(eg.EventId)).Select(eg => eg.GuestId);
                    var usersGuests = db.Guests.Where(g => eventsGuestsId.Contains(g.Id))
                        .Include(g => g.City)
                        .Include(g => g.Region).ToList();
                    return usersGuests.ToDTOGuestList();
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
            }
            return null;
        }

        public List<DTO.Guest> GetEventGuests()
        {
            using (var db = new GuestAppContext())
            {
                var eventGuests = db.EventsGuests.Where(eg => eg.EventId == _currentEvent.Id).Join(db.Guests,
                    eg => eg.GuestId,
                    g => g.Id,
                    (eventsGuests, guests) =>
                    new Model.Guest
                    {
                        Id = guests.Id,
                        GuestTitle = guests.GuestTitle,
                        FirstNames = guests.FirstNames,
                        FullName = guests.FullName,
                        LastName = guests.LastName,
                        HouseNumber = guests.HouseNumber,
                        Street = guests.Street,
                        Region = guests.Region,
                        City = guests.City,
                        Zip = guests.Zip,
                        IsFamily = guests.IsFamily
                    }).ToList();

                var orderdGuests = eventGuests.OrderBy(jg => jg.LastName).ToList();
                return orderdGuests.ToDTOGuestList();
            }
        }
        public void AddGuest(DTO.Guest newDTOGuest)
        {
            Guest newModelGuest = new Guest();
            convertToModelGuest(newDTOGuest, newModelGuest);
            try
            {
                using (var db = new GuestAppContext())
                {
                    var existingGuest = db.Guests.Find(newDTOGuest.Id);
                    if (existingGuest != null)
                        db.EventsGuests.Add(new EventGuest { EventId = _currentEvent.Id, GuestId = newDTOGuest.Id });
                    else
                        db.EventsGuests.Add(new EventGuest { EventId = _currentEvent.Id, Guest = newModelGuest });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
            }
        }
        public void EditGuestDetails()
        {
            try
            {
                using (var db = new GuestAppContext())
                {
                    var modelGuestToEdit = db.Guests.Find(UsersSelectedGuestToEdit.Id);
                    convertToModelGuest(UsersSelectedGuestToEdit, modelGuestToEdit);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var message = MessageBox.Show("There seems to be an error, make sure that there are no fields left empty!" +
                    Environment.NewLine + ex.Message,
                    "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }
        public void RemoveGuestFromCurrentEvent(DTO.Guest selectedGuest)
        {
            try
            {
                using (var db = new GuestAppContext())
                {
                    var guestToDelete = db.EventsGuests.SingleOrDefault(eg => eg.EventId == _currentEvent.Id && eg.GuestId == selectedGuest.Id);
                    db.EventsGuests.Remove(guestToDelete);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.InnerException.Message);
            }
        }

        public void DeleteGuestFromAllEvents(DTO.Guest SelectedGuest)
        {
            try
            {
                using (var db = new GuestAppContext())
                {
                    var guestToDelete = db.Guests.Find(SelectedGuest.Id);
                    db.Guests.Remove(guestToDelete);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.InnerException.Message);
            }
        }
        private void convertToModelGuest(DTO.Guest DTOGuest, Guest modelGuest)
        {
            modelGuest.GuestTitle = DTOGuest.GuestTitle;
            modelGuest.FirstNames = DTOGuest.FirstNames;
            modelGuest.LastName = DTOGuest.LastName;
            modelGuest.FullName = DTOGuest.FullName;
            modelGuest.HouseNumber = DTOGuest.HouseNumber;
            modelGuest.Street = DTOGuest.Street;
            modelGuest.RegionId = GetRegionId(DTOGuest.Region);
            modelGuest.CityId = (int)GetCityId(DTOGuest.City);
            modelGuest.Zip = DTOGuest.Zip;
            modelGuest.IsFamily = DTOGuest.IsFamily;
        }

        public int? GetRegionId(string region)
        {
            try
            {
                using (var db = new GuestAppContext())
                {
                    var dbRegion = db.Regions.SingleOrDefault(r => r.Name == region);
                    return dbRegion.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
            }
            return null;
        }
        public int? GetCityId(string city)
        {
            try
            {
                using (var db = new GuestAppContext())
                {
                    var dbCity = db.Cities.SingleOrDefault(c => c.Name == city);
                    return dbCity.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
            }
            return null;
        }


        public List<DTO.Guest> SPSearch(DTO.Guest SearchGuest)
        {
            try
            {
                using (var db = new GuestAppContext())
                {
                    return db.Guests.FromSqlRaw(String.Format("EXECUTE GuestFinder {0},{1},{2},{3},{4},{5};", _userId, _currentEvent.Id, SearchGuest.FullName, SearchGuest.Street, SearchGuest.City, SearchGuest.Region)).ToList().ToDTOGuestList();
                }
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
            }
            return null;
        }

        private static void MessageBoxError(string exMessage)
        {
            MessageBox.Show("An unexpected error occurred! " + Environment.NewLine + exMessage, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}


