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

        public int AddUser(User user)
        {
            //user.Role = "User";
            db.Users.Add(user);
            return db.SaveChanges();
        }

        public int DeleteUser(int id)
        {
            User c = db.Users.Where(x => x.UserId == id).FirstOrDefault();
            db.Users.Remove(c);
            return db.SaveChanges();
        }

        public User GetUser(int id)
        {
            return db.Users.Where(x => x.UserId == id).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public int UpdateUser(int id, User user)
        {
            User c = db.Users.Where(x => x.UserId == id).FirstOrDefault();
            c.Name = user.Name;
            c.Email = user.Email;
            c.Password = user.Password;
            c.PhoneNumber = user.PhoneNumber;
            c.Address = user.Address;
            db.Entry<User>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
