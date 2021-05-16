using GuestApp.DAL;
using GuestApp.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GuestApp.Services
{
    public class GuestDataService
    {
        private IGuestRepository _guestRepository;
        private readonly IUsersMessageService _usersMessageService;
        private Event _currentEvent;

        public GuestDataService(IGuestRepository guestRepository, IUsersMessageService usersMessageService, Event currentEvent = null)
        {
            _guestRepository = guestRepository;
            _usersMessageService = usersMessageService;
            _currentEvent = currentEvent;
        }

        public void EditGuest(DTO.Guest usersSelectedGuestToEdit)
        {
            _guestRepository.UsersSelectedGuestToEdit = usersSelectedGuestToEdit;
            Model.Guest modelGuestToEdit = _guestRepository.ModelGuestToEdit;
            var listOfChanges = ListOfChanges(usersSelectedGuestToEdit, modelGuestToEdit);
            bool canEdit = (listOfChanges.Count > 0) ? UsersConfirmation(listOfChanges) : false;
            if (canEdit)
                _guestRepository.EditGuestDetails();
        }

        private List<string> ListOfChanges(DTO.Guest selectedDTOGuest, Model.Guest modelGuestToEdit)
        {
            List<string> changes = new List<string>();
            if (modelGuestToEdit.GuestTitle != selectedDTOGuest.GuestTitle)
                changes.Add(" Title");
            if (modelGuestToEdit.FirstNames != selectedDTOGuest.FirstNames)
                changes.Add(" First names");
            if (modelGuestToEdit.LastName != selectedDTOGuest.LastName)
                changes.Add(" Family name");
            if (modelGuestToEdit.HouseNumber != selectedDTOGuest.HouseNumber)
                changes.Add(" House Number");
            if (modelGuestToEdit.Street != selectedDTOGuest.Street)
                changes.Add(" Street Name");
            if (modelGuestToEdit.RegionId.GetValueOrDefault() != _guestRepository.GetRegionId(selectedDTOGuest.Region))
                changes.Add(" Region");
            if (modelGuestToEdit.CityId != _guestRepository.GetCityId(selectedDTOGuest.City))
                changes.Add(" City Name");
            if (modelGuestToEdit.Zip != selectedDTOGuest.Zip)
                changes.Add(" Zip");
            if (modelGuestToEdit.IsFamily != selectedDTOGuest.IsFamily)
                changes.Add(" the family Check Box");

            return changes;
        }

        private bool UsersConfirmation(List<string> changes)
        {
            return _usersMessageService.UsersEditConfirmation(changes);
        }

        public void AddGuest(DTO.Guest newGuest)
        {
            if (EmptyFields(newGuest) == true)
                return;
            if (GuestExist(newGuest) == false)
                _guestRepository.AddGuest(newGuest);
            else
                _usersMessageService.GuestExistsElert();
        }

        private bool EmptyFields(DTO.Guest newGuest)
        {
            if (string.IsNullOrEmpty(newGuest.FirstNames)
                || string.IsNullOrEmpty(newGuest.LastName)
                || string.IsNullOrEmpty(newGuest.HouseNumber)
                || string.IsNullOrEmpty(newGuest.Street)
                || string.IsNullOrEmpty(newGuest.City)
                || string.IsNullOrEmpty(newGuest.Zip))
            {
                _usersMessageService.EmptyFieldsAlert();
                return true;
            }
            else if (string.IsNullOrEmpty(newGuest.Region))
                newGuest.Region = "NA";

            return false;
        }

        private bool GuestExist(DTO.Guest newGuest)
        {
            var allGuests = _guestRepository.GetEventGuests();
            if (allGuests.Any(g => g.Zip.ToUpper().Replace(" ", string.Empty).Equals(newGuest.Zip.ToUpper().Replace(" ", string.Empty)) && g.HouseNumber == newGuest.HouseNumber) == true)
                return true;
            else
                return false;
        }
        public void RemoveGuestFromCurrentEvent(DTO.Guest selectedGuest)
        {
            if (DeleteConfirmation(_currentEvent.Name))
                _guestRepository.RemoveGuestFromCurrentEvent(selectedGuest);
        }

        public void DeleteGuestFromAllEvents(DTO.Guest selectedGuest)
        {
            if (DeleteConfirmation("ALL EVENTS"))
                _guestRepository.DeleteGuestFromAllEvents(selectedGuest);
        }

        private static bool DeleteConfirmation(string eventName)
        {
            var deleteConfirmation = MessageBox.Show(string.Format("Are you sure you want to permanently" +
                " delete this guest from {0} guest list ?", eventName), "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (deleteConfirmation == MessageBoxResult.OK)
                return true;
            else
                return false;
        }
    }
}
