using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcomContext db;

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

            var cartItems = (from cartItem in db.CartItems
                             join product in db.Products on cartItem.ProductId equals product.ProductId
                             where cartItem.UserId == order.UserId
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

            foreach (var cartItem in cartItems) 
            {
                order.TotalPrice += cartItem.ProductPrice * cartItem.Quantity;
            }


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
    }
}

