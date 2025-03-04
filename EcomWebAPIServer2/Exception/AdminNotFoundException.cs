namespace EcomWebAPIServer2.Exception
{
    public class AdminNotFoundException : ApplicationException
    {
        public AdminNotFoundException() { }
        public AdminNotFoundException(string msg) : base(msg) { }
    }
}
