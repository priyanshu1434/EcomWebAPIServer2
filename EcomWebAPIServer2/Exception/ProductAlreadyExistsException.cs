namespace EcomWebAPIServer2.Exception
{
    internal class ProductAlreadyExistsException : ApplicationException
    {
        public ProductAlreadyExistsException() { }
        public ProductAlreadyExistsException(string msg) : base(msg) { }
    }
}