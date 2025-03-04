namespace EcomWebAPIServer2.Exception
{
    public class OrderAlreadyExistsException : ApplicationException
    {
         public OrderAlreadyExistsException() { }
        public OrderAlreadyExistsException(string msg) : base(msg) { }
    }
}
