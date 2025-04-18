using System.Collections.Generic;
using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Repository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(int id);
        int AddUser(User user);
        int UpdateUser(int id, User user);
        int DeleteUser(int id);
        void UpdatePassword(int userId, string newPassword);
    }
}
