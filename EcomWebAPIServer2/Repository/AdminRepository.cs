using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomWebAPIServer2.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly EcomContext db;

        public AdminRepository(EcomContext db)
        {
            this.db = db;
        }

        public int AddAdmin(Admin admin)
        {
            db.Admins.Add(admin);
            return db.SaveChanges();
        }

        public int DeleteAdmin(int id)
        {
            Admin? admin = db.Admins.FirstOrDefault(x => x.AdminId == id);
            if (admin != null)
            {
                db.Admins.Remove(admin);
                return db.SaveChanges();
            }
            throw new AdminNotFoundException($"Admin with admin id {id} does not exist");
        }

        public Admin GetAdmin(int id)
        {
            Admin? admin = db.Admins.FirstOrDefault(x => x.AdminId == id);
            if (admin == null)
            {
                throw new AdminNotFoundException($"Admin with admin id {id} does not exist");
            }
            return admin;
        }

        public List<Admin> GetAdmins()
        {
            return db.Admins.ToList();
        }

        public int UpdateAdmin(int id, Admin admin)
        {
            Admin? existingAdmin = db.Admins.FirstOrDefault(x => x.AdminId == id);
            if (existingAdmin != null)
            {
                existingAdmin.AdminName = admin.AdminName;
                existingAdmin.Password = admin.Password;
                existingAdmin.Role = admin.Role;
                db.Entry(existingAdmin).State = EntityState.Modified;
                return db.SaveChanges();
            }
            throw new AdminNotFoundException($"Admin with admin id {id} does not exist");
        }
    }
}
