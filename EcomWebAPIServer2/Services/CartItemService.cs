using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Repository;

namespace EcomWebAPIServer2.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository repo;

        public CartItemService(ICartItemRepository repo)
        {
            this.repo = repo;
        }
        public int AddCartItem(CartItem product)
        {
            if (repo.GetCartItem(product.CartItemId) != null)
            {
                throw new ProductAlreadyExistsException($"CartItem with product id {product.CartItemId} already exists");
            }
            return repo.AddCartItem(product);
        }

        public int DeleteCartItem(int id)
        {
            if (repo.GetCartItem(id) == null)
            {

                throw new ProductNotFoundException($"CartItem with product id {id} does not exists");
            }
            return repo.DeleteCartItem(id);
        }

        public CartItem GetCartItem(int id)
        {
            CartItem c = repo.GetCartItem(id);
            if (c == null)
            {
                throw new ProductNotFoundException($"CartItem with product id {id} does not exists");
            }
            return c;
        }

        public List<CartItem> GetCartItems()
        {
            return repo.GetCartItems();
        }


        public int UpdateCartItem(int id, CartItem order)
        {
            if (repo.GetCartItem(id) == null)
            {
                throw new ProductNotFoundException($"CartItem with product id {id} does not exists");
            }
            return repo.UpdateCartItem(id, order);
        }

        public object GetCartItemsByUserId(int userid)
        {
            if (repo.GetCartItemsByUserId(userid) == null)
            {
                throw new ProductNotFoundException($"CartItem with product id {userid} does not exists");
            }
            return repo.GetCartItemsByUserId(userid);
        }

    }
}
