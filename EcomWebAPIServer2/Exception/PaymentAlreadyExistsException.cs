namespace EcomWebAPIServer2.Exception
{
    public class PaymentAlreadyExistsException : ApplicationException
    {
        public PaymentAlreadyExistsException() { }
        public PaymentAlreadyExistsException(string msg) : base(msg) { }
    }
}
