using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace EcomWebAPIServer2.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcomContext db;

        private readonly CartItemService _cartItemRepository;

        public OrderRepository(EcomContext db)
        {
            this.db = db;
        }

        static int UniqueNumberGenerate()
        {
            int timestamp = (int)(DateTime.UtcNow.Ticks % 1000000000);
            int randomPart = new Random().Next(1000, 9999);
            return timestamp + randomPart;
        }
        public int AddOrder(Order order)
        {
            order.OrderId = UniqueNumberGenerate();
            order.OrderStatus = "Successfully Placed";

            var cartItems = (from cartItem in db.CartItems
                             join product in db.Products on cartItem.ProductId equals product.ProductId
                             where cartItem.UserId == order.UserId
                             select new
                             {
                                 ProductId = cartItem.ProductId,
                                 Quantity = cartItem.Quantity,
                                 ProductPrice = product.ProductPrice
                             }).ToList();

            var productDetails = cartItems.Select(cartItem => new
            {
                cartItem.ProductId,
                cartItem.Quantity
            }).ToList();

            order.ProductDetailsJson = JsonConvert.SerializeObject(productDetails);

            foreach (var cartItem in cartItems)
            {
                order.TotalPrice += cartItem.ProductPrice * cartItem.Quantity;
            }
            order.OrderDateTime = DateTime.Now;
            order.PaymentStatus = "Pending";
            db.Orders.Add(order);
            int result = db.SaveChanges();

            if (result > 0)
            {
                // Update the OrderId in the Payment table
                var payment = db.Payments.FirstOrDefault(p => p.UserId == order.UserId && p.OrderId == 0);
                if (payment != null)
                {
                    payment.OrderId = order.OrderId;
                    payment.Status = "Payment Completed";
                    db.SaveChanges();
                }

                // Truncate the CartItems table
                db.Database.ExecuteSqlRaw("TRUNCATE TABLE CartItems");
            }

            return result;
        }


        public int DeleteOrder(int id)
        {
            Order c = db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            db.Orders.Remove(c);
            return db.SaveChanges();
        }


        public Order GetOrder(int id)
        {
            return db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
        }



        public List<Order> GetOrders()
        {
            return db.Orders.ToList();

        }

        public int UpdateOrder(int id, Order order)
        {
            Order c = db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
            c.TotalPrice = order.TotalPrice;
            c.ShippingAddress = order.ShippingAddress;
            c.OrderStatus = order.OrderStatus;
            c.PaymentStatus = order.PaymentStatus;
            db.Entry<Order>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }

        public object GetOrdersByUserId(int userId)
        {
            var orders = (from order in db.Orders
                          join user in db.Users on order.UserId equals user.UserId
                          where order.UserId == userId
                          select new
                          {
                              OrderId = order.OrderId,
                              UserId = order.UserId,
                              TotalPrice = order.TotalPrice,
                              ShippingAddress = order.ShippingAddress,
                              OrderStatus = order.OrderStatus,
                              PaymentStatus = order.PaymentStatus,
                              DateTime = order.OrderDateTime,
                              ProductDetailsJson = order.ProductDetailsJson,
                              UserName = user.Name,
                              Email = user.Email,
                              Phone = user.PhoneNumber
                          }).ToList();

            var result = orders.Select(order => new
            {
                order.OrderId,
                order.UserId,
                order.TotalPrice,
                order.ShippingAddress,
                order.OrderStatus,
                order.PaymentStatus,
                order.DateTime,
                UserName = order.UserName,
                Email = order.Email,
                Phone = order.Phone,
                Products = JsonConvert.DeserializeObject<List<ProductDetail>>(order.ProductDetailsJson)
                                .Select(pd => new
                                {
                                    pd.ProductId,
                                    pd.Quantity,
                                    ProductName = db.Products.FirstOrDefault(p => p.ProductId == pd.ProductId).ProductName,
                                    ProductPrice = db.Products.FirstOrDefault(p => p.ProductId == pd.ProductId).ProductPrice,
                                    ProductDescription = db.Products.FirstOrDefault(p => p.ProductId == pd.ProductId).ProductDescription,
                                    ProductCategory = db.Products.FirstOrDefault(p => p.ProductId == pd.ProductId).ProductCategory
                                }).ToList()
            }).ToList();

            return result;
        }

        public class ProductDetail
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}

