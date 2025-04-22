namespace EcomWebAPIServer2
{
    public interface IAuth
    {
        Auth.AuthResult Authentication(string username, string password);
    }
}
