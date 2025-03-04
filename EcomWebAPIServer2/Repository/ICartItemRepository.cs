using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Repository
{
    public interface ICartItemRepository
    {
        List<User> GetCartItems();//read all the data and display 
        User GetCartItem(int id);//read single customer details
        int AddCartItem(User user);//insert
        int UpdateCartItem(int id, User user);//update
        int DeleteCartItem(int id);//delete
    }
}
