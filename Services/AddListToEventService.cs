using GuestApp.DAL;
using System.Collections.Generic;

namespace GuestApp.Services
{
    public class AddListToEventService
    {
        private IGuestRepository _guestRepository;

        public AddListToEventService(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }
        public void AddCustomList(List<DTO.Guest> customGuests)
        {

            foreach (var guest in customGuests)
            {
                var eventGuests = _guestRepository.GetEventGuests();
                if (eventGuests == null || !eventGuests.Exists(eg => eg.Id == guest.Id))
                    _guestRepository.AddGuest(guest);
            }
        }
    }
}
