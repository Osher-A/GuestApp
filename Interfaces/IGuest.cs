using System;
using System.Collections.Generic;
using System.Text;

namespace GuestApp.Interfaces
{
        public interface IGuest
        {
            string FirstNames { get; set; }
            string FullName { get; set; }
            GuestTitle GuestTitle { get; set; }
            string HouseNumber { get; set; }
            int Id { get; set; }
            bool IsFamily { get; set; }
            string LastName { get; set; }
            string Street { get; set; }
            string Zip { get; set; }
        }

    public enum GuestTitle
    {
        MrAndMrs,
        Mr,
        Mrs,
        RabbiAndMrs,
        Rabbi,
        DrAndMrs,
        Dr,
        Ms,

    }

}
