using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Services
{
    public interface IAdminService
    {
        List<Admin> GetAdmins();//read all the data and display 
        Admin GetAdmin(int id);//read single customer details
        int AddAdmin(Admin admin);//insert
        int UpdateAdmin(int id, Admin admin);//update
        int DeleteAdmin(int id);//delete
    }
}
