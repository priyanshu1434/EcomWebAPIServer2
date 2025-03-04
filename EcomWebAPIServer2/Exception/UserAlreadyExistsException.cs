namespace EcomWebAPIServer2.Exception
{
    internal class UserAlreadyExistsException : ApplicationException
    {
        public UserAlreadyExistsException() { }
        public UserAlreadyExistsException(string msg) : base(msg) { }
    }
}