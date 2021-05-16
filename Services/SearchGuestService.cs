using GuestApp.DAL;
using GuestApp.DTO;
using GuestApp.Services.Extentions;
using GuestApp.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestApp.Services
{
    public class SearchGuestService
    {
        private IGuestRepository _guestRepository;

        public SearchGuestService(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public List<DTO.Guest> GetAllGuests()
        {
            var wholeList = _guestRepository.GetGuestList();
            wholeList = wholeList.OrderBy(g => g.LastName).ToList();
            wholeList = Counter.AddCounterToGuestList(wholeList);
            return wholeList;
        }

        public List<DTO.Guest> GetEventGuests(int eventId)
        {
            var eventGuests = _guestRepository.GetEventGuests();
            eventGuests = Counter.AddCounterToGuestList(eventGuests);
            return eventGuests;
        }

        public async Task<GuestChecker> SearchListAsync(DTO.Guest searchGuest)
        {
            var fullList = await _guestRepository.GetGuestListAsync();
            var searchList = Search(searchGuest, fullList).ToList();

            searchList = Counter.AddCounterToGuestList(searchList);

            return guestChecker(fullList, searchList);
        }

        private IOrderedEnumerable<Guest> Search(Guest searchGuest, IEnumerable<Guest> fullList)
        {
            var search = fullList.WhereIf(searchGuest.FullName != null, g => g.FullName.ToUpper().Contains(searchGuest.FullName.Trim().ToUpper())).OrderBy(g => g.LastName);
            search = search.WhereIf(searchGuest.Street != null, g => g.Street.ToUpper().Contains(searchGuest.Street.Trim().ToUpper())).OrderBy(g => g.LastName);
            search = search.WhereIf(searchGuest.Region != null, g => g.Region == searchGuest.Region).OrderBy(g => g.LastName);
            search = search.WhereIf(searchGuest.City != null, g => g.City == searchGuest.City).OrderBy(g => g.LastName);
            return search;
            //return (IOrderedEnumerable<Guest>) _guestRepository.SPSearch(searchGuest);
        }

        private static GuestChecker guestChecker(IEnumerable<DTO.Guest> fullList, IEnumerable<DTO.Guest> searchList)
        {
            GuestChecker guestChecker = new GuestChecker();
            guestChecker.SearchList = searchList.ToList();

            if (guestChecker.SearchList.Count == fullList.ToList().Count || guestChecker.SearchList.Count < 1)
                guestChecker.GuestExists = false;
            else guestChecker.GuestExists = true;

            return guestChecker;
        }
    }

    public class GuestChecker
    {
        public bool GuestExists { get; set; }
        public List<DTO.Guest> SearchList { get; set; }
    }

   
}