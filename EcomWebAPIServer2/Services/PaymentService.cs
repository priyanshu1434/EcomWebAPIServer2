using EcomWebAPIServer2.Exception;
using EcomWebAPIServer2.Models;
using EcomWebAPIServer2.Repository;

namespace EcomWebAPIServer2.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepo;
        private readonly IUserRepository userRepo;

        public PaymentService(IPaymentRepository paymentRepo, IUserRepository userRepo)
        {
            this.paymentRepo = paymentRepo;
            this.userRepo = userRepo;
        }

        public int AddPayment(Payment payment)
        {
            if (userRepo.GetUser(payment.UserId) == null)
            {
                throw new UserNotFoundException($"User with ID {payment.UserId} does not exist");
            }
            if (paymentRepo.GetPayment(payment.PaymentId) != null)
            {
                throw new PaymentAlreadyExistsException($"Payment with payment id {payment.PaymentId} already exists");
            }
            return paymentRepo.AddPayment(payment);
        }

        public int DeletePayment(int id)
        {
            if (paymentRepo.GetPayment(id) == null)
            {
                throw new PaymentNotFoundException($"Payment with payment id {id} does not exist");
            }
            return paymentRepo.DeletePayment(id);
        }

        public Payment GetPayment(int id)
        {
            Payment payment = paymentRepo.GetPayment(id);
            if (payment == null)
            {
                throw new PaymentNotFoundException($"Payment with payment id {id} does not exist");
            }
            return payment;
        }

        public List<Payment> GetPayments()
        {
            return paymentRepo.GetPayments();
        }

        public int UpdatePayment(int id, Payment payment)
        {
            if (paymentRepo.GetPayment(id) == null)
            {
                throw new PaymentNotFoundException($"Payment with payment id {id} does not exist");
            }
            return paymentRepo.UpdatePayment(id, payment);
        }

        public object GetPaymentById(int id)
        {
            if (paymentRepo.GetPaymentById(id) == null)
            {
                throw new ProductNotFoundException($"Latest Payment with user id {id} does not exists");
            }
            return paymentRepo.GetPaymentById(id);
        }
    }
}