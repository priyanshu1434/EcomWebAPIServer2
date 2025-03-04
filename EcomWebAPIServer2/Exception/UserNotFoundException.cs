namespace EcomWebAPIServer2.Exception
{
    internal class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string msg) : base(msg) { }
    }
}