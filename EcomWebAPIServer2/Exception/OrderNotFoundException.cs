namespace EcomWebAPIServer2.Exception
{
    public class OrderNotFoundException : ApplicationException
    {
        public OrderNotFoundException() { }
        public OrderNotFoundException(string msg) : base(msg) { }
    }
}
