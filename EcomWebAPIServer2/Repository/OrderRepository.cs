using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Services;
using Microsoft.AspNetCore.Authentication;
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


        public int AddOrder(Order order)
        {
            db.Orders.Add(order);
            return db.SaveChanges();
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
                             join product in db.Products on order.ProductId equals product.ProductId
                             join user in db.Users on order.UserId equals user.UserId
                             join payment in db.Payments on order.PaymentId equals payment.PaymentId
                             where order.UserId == userId
                             select new
                             {
                                 OrderId = order.OrderId,
                                 UserId = order.UserId,
                                 ProductId = order.ProductId,
                                 PaymentId = order.PaymentId,
                                 TotalPrice = order.TotalPrice,
                                 ShippingAddress = order.ShippingAddress, 
                                 OrderStatus = order.OrderStatus,
                                 PaymentStatus = order.PaymentStatus,
                                 DateTime = order.OrderDateTime,
                                 ProductName = product.Name,
                                 ProductPrice = product.Price,
                                 ProductDes = product.Description,
                                 ProductCate = product.Category,
                                 UserName = user.Name,
                                 email =  user.Email,
                                 phone = user.PhoneNumber,
                                 PaymentMethod = payment.PaymentMethod

                             }).ToList();

            return orders;

        }
    }
}

