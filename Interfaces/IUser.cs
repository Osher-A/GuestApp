using System;
using System.Collections.Generic;
using System.Text;

namespace GuestApp.Interfaces
{
    public interface IUser
    {
        string Id { get; set; }
        string Email { get; set; }
    }
}
