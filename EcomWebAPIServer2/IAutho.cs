namespace EcomWebAPIServer2
{
    public interface IAutho
    {
        (string Token, int UserId)? Authentication(string email, string password);

    }
}
