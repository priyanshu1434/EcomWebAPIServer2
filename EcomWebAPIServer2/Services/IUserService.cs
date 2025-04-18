using EcomWebAPIServer2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomWebAPIServer2.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        User GetUserByEmail(string email);
        int AddUser(User user);
        int UpdateUser(int id, User user);
        int DeleteUser(int id);
        void UpdatePassword(int userId, string newPassword);
    }
}
