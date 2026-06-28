using Library_Management.Models;

namespace Library_Management.Services
{
    public interface IAuthService
    {
        Task<string> GetJWTToken(LoginModel loginModel);
    }
}
