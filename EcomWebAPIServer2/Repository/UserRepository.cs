using System;
using System.Collections.Generic;
using System.Linq;
using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EcomContext db;

        public UserRepository(EcomContext db)
        {
            this.db = db;
        }

        static int UniqueNumberGenerate()
        {
            int timestamp = (int)(DateTime.UtcNow.Ticks % 1000000000);
            int randomPart = new Random().Next(1000, 9999);
            return timestamp + randomPart;
        }

        public int AddUser(User user)
        {
            user.UserId = UniqueNumberGenerate();
            user.Role = "User";
            db.Users.Add(user);
            return db.SaveChanges();
        }

        public int DeleteUser(int id)
        {
            User userToDelete = db.Users.FirstOrDefault(x => x.UserId == id);
            if (userToDelete != null)
            {
                db.Users.Remove(userToDelete);
                return db.SaveChanges();
            }
            return 0; // Or throw an exception if deletion fails
        }

        public User GetUser(int id)
        {
            return db.Users.FirstOrDefault(x => x.UserId == id);
        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public int UpdateUser(int id, User user)
        {
            User existingUser = db.Users.FirstOrDefault(x => x.UserId == id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;
                db.Entry(existingUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return db.SaveChanges();
            }
            return 0; // Or throw an exception if update fails
        }

        public void UpdatePassword(int userId, string newPassword)
        {
            User userToUpdate = db.Users.FirstOrDefault(x => x.UserId == userId);
            if (userToUpdate != null)
            {
                userToUpdate.Password = newPassword; // In a real app, hash the password before saving
                db.Entry(userToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            // Optionally handle the case where the user is not found
        }
    }
}
