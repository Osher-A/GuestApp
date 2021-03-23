using GuestApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestApp.DAL
{
    public interface IGuestRepository
    {
        DTO.Guest UsersSelectedGuestToEdit { get; set; }
        Model.Guest ModelGuestToEdit { get; }
        List<DTO.Guest> GetGuestList();
        Task<List<DTO.Guest>> GetGuestListAsync();
       List<DTO.Guest> GetEventGuests();
        void AddGuest(DTO.Guest newDTOGuest);
        void EditGuestDetails();
        void RemoveGuestFromCurrentEvent(DTO.Guest selectedGuest);
        int? GetCityId(string city);
        int? GetRegionId(string region);
        void DeleteGuestFromAllEvents(Guest SelectedGuest);
    }
}
