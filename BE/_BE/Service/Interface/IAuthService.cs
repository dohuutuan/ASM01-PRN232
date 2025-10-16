namespace _BE.Service.Interface
{
    public interface IAuthService
    {
        string? Login(string email, string password);
    }

}
