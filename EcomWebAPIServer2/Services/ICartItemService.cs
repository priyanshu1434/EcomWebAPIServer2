using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Services
{
    public interface ICartItemService
    {
        List<CartItem> GetCartItems();//read all the data and display 
        CartItem GetCartItem(int id);//read single customer details
        int AddCartItem(CartItem item);//insert
        int UpdateCartItem(int id, CartItem item);//update
        int DeleteCartItem(int id);//delete

        object GetCartItemsByUserId(int userId);
    }
}
