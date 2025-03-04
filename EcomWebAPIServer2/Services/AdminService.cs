using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Repository;

namespace EcomWebAPIServer2.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository repo;

        public AdminService(IAdminRepository repo)
        {
            this.repo = repo;
        }

        public int AddAdmin(Admin admin)
        {
            if (repo.GetAdmin(admin.AdminId) != null)
            {
                throw new AdminAlreadyExistsException($"Admin with admin id {admin.AdminId} already exists");
            }
            return repo.AddAdmin(admin);
        }

        public int DeleteAdmin(int id)
        {
            if (repo.GetAdmin(id) == null)
            {
                throw new AdminNotFoundException($"Admin with admin id {id} does not exist");
            }
            return repo.DeleteAdmin(id);
        }

        public Admin GetAdmin(int id)
        {
            Admin admin = repo.GetAdmin(id);
            if (admin == null)
            {
                throw new AdminNotFoundException($"Admin with admin id {id} does not exist");
            }
            return admin;
        }

        public List<Admin> GetAdmins()
        {
            return repo.GetAdmins();
        }

        public int UpdateAdmin(int id, Admin admin)
        {
            if (repo.GetAdmin(id) == null)
            {
                throw new AdminNotFoundException($"Admin with admin id {id} does not exist");
            }
            return repo.UpdateAdmin(id, admin);
        }
    }
}
