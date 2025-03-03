using EcomWebAPIServer2.Models;

namespace EcomWebAPIServer2.Services
{
    public interface IPaymentService
    {
        List<Payment> GetPayments();//read all the data and display 
        Payment GetPayment(int id);//read single customer details
        int AddPayment(Payment payment);//insert
        int UpdatePayment(int id, Payment payment);//update
        int DeletePayment(int id);//delete
    }
}
