using EcomWebAPIServer2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EcomWebAPIServer2.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly EcomContext db;

        public CartItemRepository(EcomContext db)
        {
            this.db = db;
        }

        public CartItemRepository()
        {
        }

        public int AddCartItem(CartItem product)
        {
            db.CartItems.Add(product);
            return db.SaveChanges();
        }


        public int DeleteCartItem(int id)
        {
            CartItem c = db.CartItems.Where(x => x.CartItemId == id).FirstOrDefault();
            db.CartItems.Remove(c);
            return db.SaveChanges();
        }

        public CartItem GetCartItem(int id)
        {
            return db.CartItems.Where(x => x.CartItemId == id).FirstOrDefault();
        }



        public List<CartItem> GetCartItems()
        {
            return db.CartItems.ToList();

        }

        public int UpdateCartItem(int id, CartItem product)
        {
            CartItem c = db.CartItems.Where(x => x.CartItemId == id).FirstOrDefault();
            c.Quantity = product.Quantity;
            db.Entry<CartItem>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }

        public object GetCartItemsByUserId(int userId)
        {

            var cartItems = (from cartItem in db.CartItems
                             join product in db.Products on cartItem.ProductId equals product.ProductId
                             where cartItem.UserId == userId
                             select new
                             {
                                 CartItemId = cartItem.CartItemId,
                                 UserId = cartItem.UserId,
                                 ProductId = cartItem.ProductId,
                                 Quantity = cartItem.Quantity,
                                 ProductName = product.Name,
                                 ProductPrice = product.Price,
                                 ProductDes = product.Description,
                                 ProductCate = product.Category
                             }).ToList();

            return cartItems;

        }
    }
}
