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
    }
}

