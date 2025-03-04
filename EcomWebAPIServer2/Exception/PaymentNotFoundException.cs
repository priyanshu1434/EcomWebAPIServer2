namespace EcomWebAPIServer2.Exception
{
    public class PaymentNotFoundException: ApplicationException
    {
        public PaymentNotFoundException() { }
        public PaymentNotFoundException(string msg) : base(msg) { }
    }
}
