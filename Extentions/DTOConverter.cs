using System.Collections.Generic;

namespace GuestApp.Extentions
{
    public static class DTOConverter
    {
        public static List<DTO.Guest> ToDTOGuestList(this List<Model.Guest> modelGuestList)
        {
            List<DTO.Guest> DTOList = new List<DTO.Guest>();
            foreach (var modelGuest in modelGuestList)
            {
                DTO.Guest dtoGuest = new DTO.Guest();
                dtoGuest.Id = modelGuest.Id;
                dtoGuest.FirstNames = modelGuest.FirstNames;
                dtoGuest.LastName = modelGuest.LastName;
                dtoGuest.GuestTitle = modelGuest.GuestTitle;
                dtoGuest.HouseNumber = modelGuest.HouseNumber;
                dtoGuest.Street = modelGuest.Street;
                dtoGuest.Region = modelGuest.Region.Name;
                dtoGuest.City = modelGuest.City.Name;
                dtoGuest.Zip = modelGuest.Zip;
                dtoGuest.IsFamily = modelGuest.IsFamily;
                DTOList.Add(dtoGuest);
            }
            return DTOList;
        }
    }
}
