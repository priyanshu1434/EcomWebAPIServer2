namespace EcomWebAPIServer2.Exception
{
    public class AdminAlreadyExistsException : ApplicationException
    {
        public AdminAlreadyExistsException() { }
        public AdminAlreadyExistsException(string msg) : base(msg) { }
    }
}
