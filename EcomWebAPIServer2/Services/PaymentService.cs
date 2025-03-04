using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Repository;

namespace EcomWebAPIServer2.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository repo;

        public PaymentService(IPaymentRepository repo)
        {
            this.repo = repo;
        }

        public int AddPayment(Payment payment)
        {
            if (repo.GetPayment(payment.PaymentId) != null)
            {
                throw new PaymentAlreadyExistsException($"Payment with payment id {payment.PaymentId} already exists");
            }
            return repo.AddPayment(payment);
        }

        public int DeletePayment(int id)
        {
            if (repo.GetPayment(id) == null)
            {
                throw new PaymentNotFoundException($"Payment with payment id {id} does not exist");
            }
            return repo.DeletePayment(id);
        }

        public Payment GetPayment(int id)
        {
            Payment payment = repo.GetPayment(id);
            if (payment == null)
            {
                throw new PaymentNotFoundException($"Payment with payment id {id} does not exist");
            }
            return payment;
        }

        public List<Payment> GetPayments()
        {
            return repo.GetPayments();
        }

        public int UpdatePayment(int id, Payment payment)
        {
            if (repo.GetPayment(id) == null)
            {
                throw new PaymentNotFoundException($"Payment with payment id {id} does not exist");
            }
            return repo.UpdatePayment(id, payment);
        }
    }
}