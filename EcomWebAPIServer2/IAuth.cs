namespace EcomWebAPIServer2
{
    public interface IAuth
    {
        AuthResult Authentication(string username, string password);
    }
}
