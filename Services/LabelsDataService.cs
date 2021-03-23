using GuestApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestApp.Services
{
    public class LabelsDataService
    {
        private List<DTO.Guest> _labelsGuests;
        private const int _guestsPerGroup = 14;
        public LabelsDataService(List<DTO.Guest> labelsGuests)
        {
            _labelsGuests = labelsGuests;
        }
     
        public List<List<DTO.Guest>> GroupingGuests()
        {
            int numberOfGroups = NumberOfGroups();
            return GroupingGuests(numberOfGroups);
        }

        private int NumberOfGroups()
        {
            int groupCounter = 0;
            for (int i = 0; i < _labelsGuests.Count; i += 14)
                groupCounter++;

            return groupCounter;
        }

        private List<List<Guest>> GroupingGuests(int numberOfGroups)
        {
            List<List<DTO.Guest>> groupsOfGuests = new List<List<DTO.Guest>>();
            groupsOfGuests.Add(_labelsGuests.Take(_guestsPerGroup).ToList());

            int guestCounter = 14;
            for (int i = 1; i < numberOfGroups; i++)
            {
                List<DTO.Guest> group = _labelsGuests.Skip(guestCounter).Take(_guestsPerGroup).ToList();
                guestCounter += 14;
                groupsOfGuests.Add(group);
            }
            return groupsOfGuests;
        }

    }
}
