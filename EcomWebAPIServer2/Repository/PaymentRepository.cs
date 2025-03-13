using EcomWebAPIServer2.Models;
using System.Collections.Generic;
using System.Linq;

namespace EcomWebAPIServer2.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly EcomContext db;

        public PaymentRepository(EcomContext db)
        {
            this.db = db;
        }

        static int UniqueNumberGenerate()
        {
            int timestamp = (int)(DateTime.UtcNow.Ticks % 1000000000);
            int randomPart = new Random().Next(1000, 9999);
            return timestamp + randomPart;
        }

        public int AddPayment(Payment payment)
        {
            payment.PaymentId = UniqueNumberGenerate();
            payment.Status = "Pending payment - For Order Comfirmation";
            var cartItems = (from cartItem in db.CartItems
                             join product in db.Products on cartItem.ProductId equals product.ProductId
                             where cartItem.UserId == payment.UserId
                             select new
                             {
                                 CartItemId = cartItem.CartItemId,
                                 UserId = cartItem.UserId,
                                 ProductId = cartItem.ProductId,
                                 Quantity = cartItem.Quantity,
                                 ProductName = product.ProductName,
                                 ProductPrice = product.ProductPrice,
                                 ProductDes = product.ProductDescription,
                                 ProductCate = product.ProductCategory
                             }).ToList();

            foreach (var cartItem in cartItems)
            {
                payment.Amount += cartItem.ProductPrice * cartItem.Quantity;
            }
            db.Payments.Add(payment);
            return db.SaveChanges();
        }

        public int DeletePayment(int id)
        {
            Payment p = db.Payments.Where(x => x.PaymentId == id).FirstOrDefault();
            db.Payments.Remove(p);
            return db.SaveChanges();
        }

        public Payment GetPayment(int id)
        {
            return db.Payments.Where(x => x.PaymentId == id).FirstOrDefault();
        }

        public List<Payment> GetPayments()
        {
            return db.Payments.ToList();
        }

        public int UpdatePayment(int id, Payment payment)
        {
            Payment p = db.Payments.Where(x => x.PaymentId == id).FirstOrDefault();
            p.Amount = payment.Amount;
            p.Amount = payment.Amount;
            p.PaymentMethod = payment.PaymentMethod;
            p.PaymentDateTime = payment.PaymentDateTime;


            db.Entry<Payment>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }

        public object GetPaymentById(int id)
        {
            var payments = db.Payments.Where(p => p.UserId == id).ToList();

            return payments;
        }
    }
}