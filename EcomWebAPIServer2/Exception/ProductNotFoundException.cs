namespace EcomWebAPIServer2.Exception
{
    internal class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException() { }
        public ProductNotFoundException(string msg) : base(msg) { }
    }
}