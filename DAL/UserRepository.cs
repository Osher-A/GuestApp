using GuestApp.DTO;
using GuestApp.Model;
using GuestApp.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuestApp.DAL
{
    public class UserRepository
    {
        public void AddUser(DTO.User user)
        {
            var modelUser = Mapper.UserMapper(user, new Model.User());
            using (var db = new GuestAppContext())
            {
                db.Users.Add(modelUser);
            }
        }

        public DTO.User GetUser(string usersId)
        {
            Model.User modelUser;
            using (var db = new GuestAppContext())
            {
                modelUser = db.Users.Find(usersId);
            }
            var dtoUser = Mapper.UserMapper(modelUser, new DTO.User());
            return dtoUser;
        }

        public void DeleteUser(string usersId)
        {
            using (var db = new GuestAppContext())
            {
                var modelUser = db.Users.Find(usersId);
                db.Users.Remove(modelUser);
            }
        }

        public void EditUser(DTO.User user)
        {
            using (var db = new GuestAppContext())
            {
                var modelUser = db.Users.Find(user.Id);
                modelUser.Email = user.Email;
            }
        }
    }
}
