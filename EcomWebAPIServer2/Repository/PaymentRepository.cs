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

        public int AddPayment(Payment payment)
        {
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
    }
}