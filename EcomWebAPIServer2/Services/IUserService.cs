using EcomWebAPIServer2.Models;
using System.Threading.Tasks;

namespace EcomWebAPIServer2.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(int id);
        int AddUser(User user);
        int UpdateUser(int id, User user);
        int DeleteUser(int id);
        Task<User> GetUserByEmailAsync(string email); // New method
    }
}
